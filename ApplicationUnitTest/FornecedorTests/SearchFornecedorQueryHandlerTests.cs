using ApplicationCore.CadNotasCore.Fornecedors.Queries;
using ApplicationCore.CadNotasCore.Fornecedors.Specification;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationUnitTest.FornecedorTests
{
    public class SearchFornecedorQueryHandlerTests
    {
        private readonly Mock<IRepositoryCad<CadFornecedor>> _repositoryMock;
        private readonly SearchFornecedorQueryHandler _handler;

        public SearchFornecedorQueryHandlerTests()
        {
            _repositoryMock = new Mock<IRepositoryCad<CadFornecedor>>();
            _handler = new SearchFornecedorQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnPagedResult_When_FornecedoresExist()
        {
            var fornecedors = new List<FornecedorVM>
            {
                new()
                {
                    Id = 1,
                    Fornecedor = "Microsoft",
                }
            };

            _repositoryMock.Setup(x => x.ListAsync(It.IsAny<CadFornecedorSpecificaiton>(),It.IsAny<CancellationToken>())).ReturnsAsync(fornecedors);

            _repositoryMock.Setup(x => x.CountAsync(It.IsAny<CountSpecification<CadFornecedor>>())).ReturnsAsync(1);

            var query = new SearchFornecedorQuery
            {
                Filter = "Micro",
                Ativo = true,
                PageNo = 1
            };

            var result = await _handler.Handle(query,CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.TotalCount.Should().Be(1);
            result.Value.Items.Should().HaveCount(1);

            _repositoryMock.Verify(x => x.ListAsync(It.IsAny<CadFornecedorSpecificaiton>(),It.IsAny<CancellationToken>()),Times.Once);
            _repositoryMock.Verify(x => x.CountAsync(It.IsAny<CountSpecification<CadFornecedor>>()),Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_When_NoResultsFound()
        {
            _repositoryMock.Setup(x => x.ListAsync(It.IsAny<CadFornecedorSpecificaiton>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<FornecedorVM>());

            var query = new SearchFornecedorQuery
            {
                Filter = "Micro",
                Ativo = true,
                PageNo = 1
            };
            var result = await _handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            _repositoryMock.Verify(x => x.CountAsync(It.IsAny<CountSpecification<CadFornecedor>>()),Times.Never);

        }

    }
}
