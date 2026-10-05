using ApplicationCore.CadNotasCore.Disciplinas.Specifications;
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

namespace ApplicationCore.CadNotasCore.Disciplinas.Queries
{
    [Authorize]
    public record SearchDisciplinas : SearchDTO, IRequest<Result<PagedResult<DisciplinaVM>>>;

    public class SearchDisciplinaQueryHandler(IRepositoryCad<EntityDisciplina> repos) : IRequestHandler<SearchDisciplinas, Result<PagedResult<DisciplinaVM>>>
    {
        private readonly IRepositoryCad<EntityDisciplina> _repository = repos;
        public async Task<Result<PagedResult<DisciplinaVM>>> Handle(SearchDisciplinas query, CancellationToken cancellationToken)
        {
            var items = await _repository.ListAsync(new CadDisciplinaSpecification(query.Filter, query.AtivoValue, query.PageNo), cancellationToken);
            if (items.Count == 0) return Result<PagedResult<DisciplinaVM>>.Failure(new Error("ERR404", "No result found."));

            var predicate = CadDisciplinaSpecification.BuildFilter(query.Filter,query.AtivoValue);
            var totalCount = await _repository.CountAsync(new CountSpecification<EntityDisciplina>(predicate), cancellationToken);

            return Result<PagedResult<DisciplinaVM>>.Success(new PagedResult<DisciplinaVM>
            {
                Items = items,
                Page = query.PageNo,
                TotalCount = totalCount
            });
        }
    }



}
