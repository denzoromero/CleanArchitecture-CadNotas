using FluentAssertions;
using Infrastructure.DataCad;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace FunctionalTests.CadFornecedorTests
{
    public class EditFornecedorTests : IClassFixture<CadNotasWebFactory>
    {
        private readonly HttpClient _client;
        private readonly CadNotasWebFactory _factory;

        public EditFornecedorTests(CadNotasWebFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task EditFornecedor_Should_Return_Success()
        {
            var command = new
            {
                Id = 1,
                IdempotencyKey = Guid.NewGuid(),
                Fornecedor = "Microsoft",
                Fantasia = "MS",
                IE = "123",
                CNPJ = "456"
            };

            var response = await _client.PostAsJsonAsync("/Fornecedor/Edit", command);

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ContextCad>();
            var fornecedor = await context.CadFornecedors.FirstOrDefaultAsync(x => x.Id == 1);

            fornecedor.Should().NotBeNull();

            response.StatusCode.Should().NotBe(HttpStatusCode.Redirect);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task EditFornecedor_Should_Return_BadRequest_When_IdempotencyKey_Is_Empty()
        {
            var command = new
            {
                IdempotencyKey = Guid.Empty
            };

            var response =
                await _client.PostAsJsonAsync(
                    "/Fornecedor/Edit",
                    command);

            response.StatusCode.Should()
                .Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task EditFornecedor_Should_Return_BadRequest_When_Id_Is_Empty()
        {
            var command = new
            {
                Id = 0
            };

            var response =
                await _client.PostAsJsonAsync(
                    "/Fornecedor/Edit",
                    command);

            response.StatusCode.Should()
                .Be(HttpStatusCode.BadRequest);
        }

    }
}
