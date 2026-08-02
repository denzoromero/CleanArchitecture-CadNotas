using ApplicationCore.CadNotasCore.Transportadoras.Specifications;
using ApplicationCore.Common;
using ApplicationCore.Common.DTO;
using ApplicationCore.Common.Security;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadTransportadora;
using MediatR;

namespace ApplicationCore.CadNotasCore.Transportadoras.Queries
{
    [Authorize]
    public record SearchTransportadora : SearchDTO, IRequest<Result<PagedResult<TransportadoraVM>>>;

    public class SearchTransportadoraQueryHandler(IRepositoryCad<CadTransportadora> repos) : IRequestHandler<SearchTransportadora, Result<PagedResult<TransportadoraVM>>>
    {
        private readonly IRepositoryCad<CadTransportadora> _repository = repos;
        public async Task<Result<PagedResult<TransportadoraVM>>> Handle (SearchTransportadora req, CancellationToken cancellationToken)
        {
            var items = await _repository.ListAsync(new CadTransportadoraSpecification(req.Filter, req.AtivoValue, req.PageNo), cancellationToken);
            if (items.Count == 0) return Result<PagedResult<TransportadoraVM>>.Failure(new Error("ERR404", "No result found."));

            var predicate = CadTransportadoraSpecification.BuildFilter(req.Filter, req.AtivoValue);
            var totalCount = await _repository.CountAsync(new CountSpecification<CadTransportadora>(predicate));

            return Result<PagedResult<TransportadoraVM>>.Success(new PagedResult<TransportadoraVM>
            {
                Items = items,
                Page = req.PageNo,
                TotalCount = totalCount,
            });
        }
    }

}
