using ApplicationCore.Common.DTO;
using ApplicationCore.Interfaces;
using DocumentFormat.OpenXml.InkML;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using Infrastructure;
using Infrastructure.DataBS;
using Infrastructure.DataCad;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureIntegrationTests.InterceptorTests
{
    public class AuditInterceptorTests 
    {
        private readonly ContextCad _context;
        private readonly Mock<IAuditLogger> _loggerMock;

        public AuditInterceptorTests()
        {
            _loggerMock = new Mock<IAuditLogger>();

            var interceptor = new AuditInterceptor(_loggerMock.Object);
            var options = new DbContextOptionsBuilder<ContextCad>().UseInMemoryDatabase(Guid.NewGuid().ToString()).AddInterceptors(interceptor).Options;

            _context = new ContextCad(options);
        }

        [Fact]
        public async Task Should_Log_Added_Action()
        {
            var fornecedor = CadFornecedor.Create(BuildFornecedor("microsoft"));
            await _context.AddAsync(fornecedor);
            await _context.SaveChangesAsync();

            _loggerMock.Verify(x => x.LogAsync(It.Is<AuditEntry>(e => e.EntityName == nameof(CadFornecedor) && e.Action == "Added")), Times.Once);

        }

        [Fact]
        public async Task Should_Log_Modified_Action()
        {

            var fornecedor = CadFornecedor.Create(BuildFornecedor("microsoft"));
            await _context.AddAsync(fornecedor);
            await _context.SaveChangesAsync();

            _loggerMock.Invocations.Clear();

            fornecedor.Update(BuildFornecedor("Oracle"));

            await _context.SaveChangesAsync();

            _loggerMock.Verify(x => x.LogAsync(It.Is<AuditEntry>(e => e.Action == "Modified")), Times.Once);
        }

        [Fact]
        public async Task Should_Log_Delete_Action()
        {
            var fornecedor = CadFornecedor.Create(BuildFornecedor("microsoft"));
            await _context.AddAsync(fornecedor);
            await _context.SaveChangesAsync();

            _loggerMock.Invocations.Clear();

            _context.CadFornecedors.Remove(fornecedor);
            await _context.SaveChangesAsync();

            _loggerMock.Verify(x => x.LogAsync(It.Is<AuditEntry>(e =>e.Action == "Deleted")),Times.Once);
        }


        private static FornecedorPO BuildFornecedor(string fornecedor)
        {
            return new FornecedorPO(
                   Fornecedor: fornecedor,
                Fantasia: "MS",
                Contato: "John",
                IE: "123",
                CNPJ: "456",
                Rua: "Main Street",
                Numero: "10",
                Bairro: "Centro",
                IdCidade: 1,
                IdEstado: 1,
                Cep: "20000-000",
                Telefone1: "999999",
                Telefone2: null,
                EMail: "test@test.com",
                HomePage: "www.test.com",
                IdUser: 1);
        }
    }
}
