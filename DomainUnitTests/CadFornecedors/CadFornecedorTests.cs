using Domain.Entities.EntitiesCad.ECadFornecedor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainUnitTests.CadFornecedors
{
    public class CadFornecedorTests
    {
        [Fact]
        public void Create_Should_CreateFornecedor_When_DataIsValid()
        {
            // Arrange
            var param = new FornecedorPO(
                Fornecedor: "Microsoft",
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

            // Act
            var fornecedor = CadFornecedor.Create(param);

            // Assert
            fornecedor.Should().NotBeNull();

            fornecedor.Fornecedor.Should().Be("Microsoft");

            fornecedor.Fantasia.Should().Be("MS");

            fornecedor.Usuario.Should().Be(1);

            fornecedor.Ativo.Should().Be(1);
        }

        [Fact]
        public void Create_Should_Set_DataRegistro()
        {
            // Arrange
            var param = new FornecedorPO(
                       Fornecedor: "Microsoft",
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

            // Act
            var fornecedor = CadFornecedor.Create(param);

            // Assert
            fornecedor.DataRegistro.Should().NotBe(default);
        }

        [Fact]
        public void Create_Should_Throw_When_Fornecedor_IsEmpty()
        {
            // Arrange
            var param = new FornecedorPO(
                      Fornecedor: "",
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

            // Act
            Action act = () => CadFornecedor.Create(param);

            // Assert
            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void Create_Should_Throw_When_UserId_IsZero()
        {
            // Arrange
            var param = new FornecedorPO(
                   Fornecedor: "",
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
                   IdUser: 0);

            // Act
            Action act = () => CadFornecedor.Create(param);

            // Assert
            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void Update_Should_Change_Properties()
        {
            // Arrange
            var fornecedor = CadFornecedor.Create(
                BuildValidFornecedor());

            //var update = BuildValidFornecedor();
            var update = new FornecedorPO(
            Fornecedor: "Oracle",
            Fantasia: "ORCL",
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
            IdUser: 0);

            //update.Fornecedor = "Oracle";
            //update.Fantasia = "ORCL";

            // Act
            fornecedor.Update(update);

            // Assert
            fornecedor.Fornecedor.Should().Be("Oracle");

            fornecedor.Fantasia.Should().Be("ORCL");
        }




        private static FornecedorPO BuildValidFornecedor()
        {
            return new FornecedorPO(
                   Fornecedor: "Microsoft",
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
