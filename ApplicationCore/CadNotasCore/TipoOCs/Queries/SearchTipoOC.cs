using ApplicationCore.CadNotasCore.TipoOCs.Specifications;
using ApplicationCore.Common;
using ApplicationCore.Common.DTO;
using ApplicationCore.Common.Security;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.TipoOCs.Queries
{
    [Authorize]
    public record SearchTipoOC : SearchDTO, IRequest<Result<PagedResult<TipoOCVM>>>;

    public class SearchTipoOCQueryHandler(IRepositoryCad<CadTipoOC> repos) : IRequestHandler<SearchTipoOC, Result<PagedResult<TipoOCVM>>>
    {
        private readonly IRepositoryCad<CadTipoOC> _repository = repos;
        public async Task<Result<PagedResult<TipoOCVM>>> Handle(SearchTipoOC query, CancellationToken cancellationToken)
        {
            var items = await _repository.ListAsync(new CadTipoOCSpecifcation(query.Filter, query.AtivoValue, query.PageNo), cancellationToken);
            if (items.Count == 0) return Result<PagedResult<TipoOCVM>>.Failure(new Error("404", "No result found."));

            var predicate = CadTipoOCSpecifcation.BuildFilter(query.Filter, query.AtivoValue);
            var totalCount = await _repository.CountAsync(new CountSpecification<CadTipoOC>(predicate), cancellationToken);

            return Result<PagedResult<TipoOCVM>>.Success(new PagedResult<TipoOCVM>
            {
                Items = items,
                Page = query.PageNo,
                TotalCount = totalCount
            });
        }
    }
}
