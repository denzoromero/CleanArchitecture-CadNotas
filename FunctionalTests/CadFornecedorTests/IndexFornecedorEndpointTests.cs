using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FunctionalTests.CadFornecedorTests
{
    public class IndexFornecedorEndpointTests : IClassFixture<CadNotasWebFactory>
    {
        private readonly HttpClient _client;
        private readonly CadNotasWebFactory _factory;

        public IndexFornecedorEndpointTests(CadNotasWebFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Index_Should_Return_Ok()
        {
            // Act
            var response = await _client.GetAsync("/Fornecedor/Index");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Index_Should_Render_Page()
        {
            // Act
            var response = await _client.GetAsync("/Fornecedor/Index");

            var html = await response.Content.ReadAsStringAsync();

            // Assert
            html.Should().Contain("Fornecedor");
        }

    }
}
