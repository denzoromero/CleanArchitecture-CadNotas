using ApplicationCore.CadNotasCore.Fornecedors.Specification;
using ApplicationCore.Common;
using ApplicationCore.Common.DTO;
using ApplicationCore.Common.Security;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.CadNotasCore.Fornecedors.Queries
{
    [Authorize]
    public record class SearchFornecedorQuery : SearchDTO, IRequest<Result<PagedResult<FornecedorVM>>>;

    public class SearchFornecedorQueryHandler(IRepositoryCad<CadFornecedor> repos) : IRequestHandler<SearchFornecedorQuery, Result<PagedResult<FornecedorVM>>>
    {
        private readonly IRepositoryCad<CadFornecedor> _repository = repos;

        public async Task<Result<PagedResult<FornecedorVM>>> Handle(SearchFornecedorQuery request,CancellationToken cancellationToken)
        {
            var fornecedors = await _repository.ListAsync(new CadFornecedorSpecificaiton(request.Filter, request.AtivoValue, request.PageNo), cancellationToken);
            if (fornecedors.Count == 0) return Result<PagedResult<FornecedorVM>>.Failure(new Error("ERR404", "No result found."));

            var predicate = CadFornecedorSpecificaiton.BuildFilter(request.Filter,request.AtivoValue);
            var totalCount = await _repository.CountAsync(new CountSpecification<CadFornecedor>(predicate));

            return Result<PagedResult<FornecedorVM>>.Success(new PagedResult<FornecedorVM>
            {
                Items = fornecedors,
                Page = request.PageNo,
                TotalCount = totalCount,
            });
        }
    }

}
