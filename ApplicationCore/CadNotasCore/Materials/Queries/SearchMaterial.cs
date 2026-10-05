using ApplicationCore.CadNotasCore.Materials.Specifications;
using ApplicationCore.Common;
using ApplicationCore.Common.DTO;
using ApplicationCore.Common.Security;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadMaterial;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Materials.Queries
{
    [Authorize]
    public record SearchMaterial : SearchDTO, IRequest<Result<PagedResult<MaterialVM>>>;

    public class SearchMaterialQueryHandler(IRepositoryCad<EntityMaterial> repos) : IRequestHandler<SearchMaterial, Result<PagedResult<MaterialVM>>>
    {
        private readonly IRepositoryCad<EntityMaterial> _repository = repos;

        public async Task<Result<PagedResult<MaterialVM>>> Handle(SearchMaterial query, CancellationToken cancellationToken)
        {
            var items = await _repository.ListAsync(new SearchMaterialSpecification(query.Filter, query.AtivoValue, query.PageNo));
            if (items.Count == 0) return Result<PagedResult<MaterialVM>>.Failure(new Error("ERR404", "No result found."));

            var predicate = SearchMaterialSpecification.BuildFilter(query.Filter, query.AtivoValue);
            var totalCount = await _repository.CountAsync(new CountSpecification<EntityMaterial>(predicate));

            return Result<PagedResult<MaterialVM>>.Success(new PagedResult<MaterialVM>
            {
                Items = items,
                Page = query.PageNo,
                TotalCount = totalCount
            });

        }
    }

}
