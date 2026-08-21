using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace FunctionalTests.CadFornecedorTests
{
    public class FornecedorAuthorizationTests : IClassFixture<UnauthorizedWebFactory>
    {
        private readonly HttpClient _client;

        public FornecedorAuthorizationTests(UnauthorizedWebFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task CreateFornecedor_Should_Require_Authorization()
        {
            // Arrange
            var command = new
            {
                IdempotencyKey = Guid.NewGuid(),
                Fornecedor = "Microsoft",
                Fantasia = "MS",
                IE = "123",
                CNPJ = "456"
            };
 
            // Act
            var response = await _client.PostAsJsonAsync("/Fornecedor/Insert",command);
 
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);

            response.Headers.Location.Should().NotBeNull();
            response.Headers.Location!.AbsolutePath.Should().Be("/Home/Login");
        }

        [Fact]
        public async Task Index_Should_Require_Authorization()
        {
            // Act
            var response = await _client.GetAsync(
                "/Fornecedor/Index");

            // Assert
            response.StatusCode.Should()
                .Be(HttpStatusCode.Redirect);

            response.Headers.Location.Should()
                .NotBeNull();

            response.Headers.Location!.AbsolutePath.Should()
                .Be("/Home/Login");
        }

        [Fact]
        public async Task Search_Should_Require_Authorization()
        {
            var response = await _client.GetAsync(
                "/Fornecedor/Search");

            response.StatusCode.Should()
                .Be(HttpStatusCode.Redirect);

            response.Headers.Location.Should()
                .NotBeNull();

            response.Headers.Location!.AbsolutePath.Should()
                .Be("/Home/Login");
        }

    }
}
