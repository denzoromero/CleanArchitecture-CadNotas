using ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries.VMs;
using ApplicationCore.CadNotasCore.NotaFiscaisCore.Specifications;
using ApplicationCore.CadNotasCore.Procedimentos.Queries;
using ApplicationCore.CadNotasCore.Procedimentos.Specifications;
using ApplicationCore.Common;
using ApplicationCore.Common.DTO;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries
{
    public record SearchNotaFiscais : SearchDTO, ISearchObra, IRequest<Result<PagedResult<NotaFiscaisVM>>>
    {
        public int IdObra { get; init; }
    }

    public class SearchNotaFiscaisQueryHandler(IRepositoryCad<EntityLVM> repos) : IRequestHandler<SearchNotaFiscais, Result<PagedResult<NotaFiscaisVM>>>
    {
        private readonly IRepositoryCad<EntityLVM> _repository = repos;
        public async Task<Result<PagedResult<NotaFiscaisVM>>> Handle(SearchNotaFiscais query, CancellationToken cancellationToken)
        {
            var items = await _repository.ListAsync(new SearchNotaFiscaisSpecification(query.Filter, query.AtivoValue,query.IdObra, query.PageNo), cancellationToken);
            if (items.Count == 0) return Result<PagedResult<NotaFiscaisVM>>.Failure(new Error("ERR404", "No result found."));

            var predicate = SearchNotaFiscaisSpecification.BuildFilter(query.Filter, query.AtivoValue, query.IdObra);
            var totalCount = await _repository.CountAsync(new CountSpecification<EntityLVM>(predicate), cancellationToken);

            return Result<PagedResult<NotaFiscaisVM>>.Success(new PagedResult<NotaFiscaisVM>
            {
                Items = items,
                Page = query.PageNo,
                TotalCount = totalCount
            });
        }


    }



}
