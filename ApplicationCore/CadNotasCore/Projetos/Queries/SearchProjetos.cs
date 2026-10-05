using ApplicationCore.CadNotasCore.Projetos.Specifications;
using ApplicationCore.CadNotasCore.Transportadoras;
using ApplicationCore.CadNotasCore.Transportadoras.Specifications;
using ApplicationCore.Common;
using ApplicationCore.Common.DTO;
using ApplicationCore.Common.Security;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadProjeto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Projetos.Queries
{
    [Authorize]
    public record SearchProjetos : SearchDTO, IRequest<Result<PagedResult<ProjetosVM>>>;

    public class SearchProjetosQueryHandler(IRepositoryCad<EntityProjeto> repos) : IRequestHandler<SearchProjetos, Result<PagedResult<ProjetosVM>>>
    {
        private readonly IRepositoryCad<EntityProjeto> _repository = repos;
        public async Task<Result<PagedResult<ProjetosVM>>> Handle(SearchProjetos query, CancellationToken cancellationToken)
        {
            var items = await _repository.ListAsync(new SearchProjetosSpecification(query.Filter, query.AtivoValue, query.PageNo), cancellationToken);
            if (items.Count == 0) return Result<PagedResult<ProjetosVM>>.Failure(new Error("ERR404", "No result found."));

            var predicate = SearchProjetosSpecification.BuildFilter(query.Filter, query.AtivoValue);
            var totalCount = await _repository.CountAsync(new CountSpecification<EntityProjeto>(predicate), cancellationToken);

            return Result<PagedResult<ProjetosVM>>.Success(new PagedResult<ProjetosVM>
            {
                Items = items,
                Page = query.PageNo,
                TotalCount = totalCount,
            });

        }
    }
}
