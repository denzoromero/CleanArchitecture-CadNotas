using ApplicationCore.CadNotasCore.Fornecedors.Queries;
using ApplicationCore.CadNotasCore.Fornecedors.Specification;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using DocumentFormat.OpenXml.InkML;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using FluentAssertions;
using Infrastructure;
using Infrastructure.DataBS;
using Infrastructure.DataCad;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureIntegrationTests.CadFornecedorTests
{
    public class CadFornecedorSpecificationTests : IClassFixture<TestCadNotasFixture>
    {
        private readonly ContextCad _context;
        public CadFornecedorSpecificationTests(TestCadNotasFixture fixture)
        {
            _context = fixture.ContextCad;
        }

        [Fact]
        public async Task Should_Return_Active_Fornecedores()
        {
            _context.CadFornecedors.Add(CadFornecedor.Create(BuildValidFornecedor("Microsoft")));
            await _context.SaveChangesAsync();

            var repository = new EfRepositoryCad<CadFornecedor>(_context);
 
            var spec = new CadFornecedorSpecificaiton(null,1);
 
            var result = await repository.ListAsync(spec);
 
            result.Should().NotBeEmpty();
            result.First().Ativo.Should().Be(1);
        }

        [Fact]
        public async Task Should_Filter_By_Fornecedor_Name()
        {
            _context.CadFornecedors.Add(CadFornecedor.Create(BuildValidFornecedor("Microsoft")));
            await _context.SaveChangesAsync();

            var repository = new EfRepositoryCad<CadFornecedor>(_context);
            var spec = new CadFornecedorSpecificaiton("Microsoft", 1);

            var result = await repository.ListAsync(spec);
            result.Should().ContainSingle();
            result.First().Fornecedor.Should().Be("Microsoft");
        }

        [Fact]
        public async Task Should_Filter_By_Pagination()
        {
            for (int i = 1; i <= 3; i++)
            {
                _context.CadFornecedors.Add(CadFornecedor.Create(BuildValidFornecedor($"Fornecedor {i}")));
            }
            await _context.SaveChangesAsync();

            var repository = new EfRepositoryCad<CadFornecedor>(_context);

            var spec = new CadFornecedorSpecificaiton(null, 1, 2);

            var result = await repository.ListAsync(spec);

            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task AddAsync_Should_Save_Fornecedor()
        {

            // Arrange
            var repository = new EfRepositoryCad<CadFornecedor>(_context);
 
            var fornecedor = CadFornecedor.Create(BuildValidFornecedor("Microsoft"));
 
            // Act
            await repository.AddAsync(fornecedor);
            await _context.SaveChangesAsync();
 
            // Assert
            var saved = await _context.CadFornecedors.FirstOrDefaultAsync(x => x.Fornecedor == fornecedor.Fornecedor);
 
            saved.Should().NotBeNull();
            saved!.Fornecedor.Should().Be(fornecedor.Fornecedor);

        }

        [Fact]
        public async Task Update_Should_Persist_Changes()
        {
            // Arrange
            var repository = new EfRepositoryCad<CadFornecedor>(_context);

            var fornecedor = CadFornecedor.Create(BuildValidFornecedor("microsoft"));

            await repository.AddAsync(fornecedor);
            await _context.SaveChangesAsync();

            fornecedor.Update(BuildValidFornecedor("Oracle"));

            // Act
            await _context.SaveChangesAsync();

            // Assert
            var updated = await _context.CadFornecedors.FirstAsync(x => x.Id == fornecedor.Id);

            updated.Fornecedor.Should().Be("Oracle");
        }


        private static FornecedorPO BuildValidFornecedor(string fornecedor)
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
