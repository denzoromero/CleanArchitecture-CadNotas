using ApplicationCore.CadNotasCore.Obras.Specifications;
using ApplicationCore.Common;
using ApplicationCore.Common.DTO;
using ApplicationCore.Common.Security;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadObra;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Obras.Queries
{
    [Authorize]
    public record SearchObra : SearchDTO, IRequest<Result<PagedResult<ObraVM>>>;

    public class SearchObraQueryHandler(IRepositoryCad<CadObra> repos) : IRequestHandler<SearchObra, Result<PagedResult<ObraVM>>>
    {
        private readonly IRepositoryCad<CadObra> _repos = repos;

        public async Task<Result<PagedResult<ObraVM>>> Handle(SearchObra req, CancellationToken cancellation)
        {
            var items = await _repos.ListAsync(new SearchObraSpecification(req.Filter, req.AtivoValue, req.PageNo), cancellation);
            if (items.Count == 0) return Result<PagedResult<ObraVM>>.Failure(new Error("ERR404", "No result found."));

            var predicate = SearchObraSpecification.BuildFilter(req.Filter, req.AtivoValue);
            var totalCount = await _repos.CountAsync(new CountSpecification<CadObra>(predicate), cancellation);

            return Result<PagedResult<ObraVM>>.Success(new PagedResult<ObraVM> { 
                Items = items,
                Page = req.PageNo,
                TotalCount = totalCount,
            });
        }
    }

}
