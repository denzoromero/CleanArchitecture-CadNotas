using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FunctionalTests.CadFornecedorTests
{
    public class SearchFornecedorEndpointTests : IClassFixture<CadNotasWebFactory>
    {
        private readonly HttpClient _client;
        private readonly CadNotasWebFactory _factory;

        public SearchFornecedorEndpointTests(CadNotasWebFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Search_Should_Return_NoResult()
        {
            // Act
            var response = await _client.GetAsync("/Fornecedor/Search");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Search_Should_Return_Filter_Fornecedor()
        {
            // Act
            var response = await _client.GetAsync("/Fornecedor/Search?Filter=Samsung&Ativo=true");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("Samsung");
        }

        [Fact]
        public async Task Search_Should_Return_BadRequest_When_No_Data_Found()
        {
            // Act
            var response = await _client.GetAsync("/Fornecedor/Search?Filter=FornecedorInexistente");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


    }
}
