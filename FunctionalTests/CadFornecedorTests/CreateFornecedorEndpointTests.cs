using ApplicationCore.CadNotasCore.Fornecedors.Commands.Create;
using ApplicationCore.CadNotasCore.Fornecedors.Queries;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using FluentAssertions;
using Infrastructure.DataCad;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace FunctionalTests.CadFornecedorTests
{
    public class CreateFornecedorEndpointTests : IClassFixture<CadNotasWebFactory>
    {
        private readonly HttpClient _client;
        private readonly CadNotasWebFactory _factory;

        public CreateFornecedorEndpointTests(CadNotasWebFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task CreateFornecedor_Should_Return_Success()
        {
            var command = new
            {
                IdempotencyKey = Guid.NewGuid(),
                Fornecedor = "Microsoft",
                Fantasia = "MS",
                IE = "123",
                CNPJ = "456"
            };

            var response = await _client.PostAsJsonAsync("/Fornecedor/Insert", command);

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ContextCad>();
            var fornecedor = await context.CadFornecedors.FirstOrDefaultAsync(x => x.Fornecedor == "Microsoft");

            fornecedor.Should().NotBeNull();

            response.StatusCode.Should().NotBe(HttpStatusCode.Redirect);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateFornecedor_Should_Return_BadRequest_When_IdempotencyKey_Is_Empty()
        {
            var command = new
            {
                IdempotencyKey = Guid.Empty
            };

            var response =
                await _client.PostAsJsonAsync(
                    "/Fornecedor/Insert",
                    command);

            response.StatusCode.Should()
                .Be(HttpStatusCode.BadRequest);
        }
    }
}
