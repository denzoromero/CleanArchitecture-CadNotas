using ApplicationCore.CadNotasCore.MaterialCLVMs.Queries;
using ApplicationCore.CadNotasCore.MaterialCLVMs.Specifications;
using ApplicationCore.CadNotasCore.Procedimentos.Specifications;
using ApplicationCore.Common;
using ApplicationCore.Common.DTO;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Procedimentos.Queries
{
    public record SearchProcedimentos : SearchDTO, IRequest<Result<PagedResult<ProcedimentoVM>>>;

    public class SearchProcedimentosQueryHandler(IRepositoryCad<CadProcedimento> repos) : IRequestHandler<SearchProcedimentos, Result<PagedResult<ProcedimentoVM>>>
    {
        private readonly IRepositoryCad<CadProcedimento> _repository = repos;

        public async Task<Result<PagedResult<ProcedimentoVM>>> Handle(SearchProcedimentos query, CancellationToken cancellationToken)
        {
            var itemcheck = await _repository.ListAsync(cancellationToken);

            var items = await _repository.ListAsync(new SearchProcedimentoSpecification(query.Filter, query.AtivoValue, query.PageNo));
            if (items.Count == 0) return Result<PagedResult<ProcedimentoVM>>.Failure(new Error("ERR404", "No result found."));

            var predicate = SearchProcedimentoSpecification.BuildFilter(query.Filter, query.AtivoValue);
            var totalCount = await _repository.CountAsync(new CountSpecification<CadProcedimento>(predicate));

            return Result<PagedResult<ProcedimentoVM>>.Success(new PagedResult<ProcedimentoVM>
            {
                Items = items,
                Page = query.PageNo,
                TotalCount = totalCount
            });

        }

    }

}
