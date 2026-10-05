using ApplicationCore.CadNotasCore.Fornecedors.Commands.Create;
using ApplicationCore.CadNotasCore.Fornecedors.Queries;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationUnitTest.FornecedorTests
{
    public class CreateFornecedorCommandHandlerTests
    {
        private readonly Mock<IRepositoryCad<EntityFornecedor>> _repositoryMock;
        private readonly Mock<IUser> _userMock;
        private readonly CreateFornecedorCommandHandler _handler;

        public CreateFornecedorCommandHandlerTests()
        {
            _repositoryMock = new Mock<IRepositoryCad<EntityFornecedor>>();
            _userMock = new Mock<IUser>();
            _userMock.Setup(x => x.UserId).Returns(1);
            _handler = new CreateFornecedorCommandHandler(_repositoryMock.Object,_userMock.Object);
        }


        [Fact]
        public async Task Handle_Should_CreateFornecedor_When_RequestIsValid()
        {
            // Arrange
            var command = new CreateFornecedor
            {
                IdempotencyKey = Guid.NewGuid(),
                Fornecedor = "Fornecedor Teste",
                Fantasia = "Fantasia Teste",
                IE = "abcd",
                Contato = "João",
                CNPJ = "123456789",
                Rua = "Rua A",
                Numero = "100",
                Bairro = "Centro",
                IdCidade = 1,
                IdEstado = 1,
                Cep = "12345-000",
                Telefone1 = "999999999"
            };

            var result = await _handler.Handle(command,CancellationToken.None);

            result.Should().NotBeNull();

            result.IsSuccess.Should().BeTrue();

            result.Message.Should().Contain("Fornecedor Successfully inserted.");

            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<EntityFornecedor>(),It.IsAny<CancellationToken>()),Times.Once);

        }

    }
}
