using ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries.VMs;
using ApplicationCore.Common;
using ApplicationCore.Common.DTO;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.RDFAs.Queries.GetLVMRDFA
{
    public record GetLVMRDFAQuery : SearchDTO, ISearchObra, IRequest<Result<PagedResult<GetLVMRDFAVM>>>
    {
        public int IdObra { get; init; }
    }

    public class GetLVMRDFAQueryHandler(IRepositoryCad<CadLVM> repos) : IRequestHandler<GetLVMRDFAQuery, Result<PagedResult<GetLVMRDFAVM>>>
    {
        private readonly IRepositoryCad<CadLVM> _repos = repos;
        public async Task<Result<PagedResult<GetLVMRDFAVM>>> Handle (GetLVMRDFAQuery query, CancellationToken cancellationToken)
        {
            var items = await _repos.ListAsync(new GetLVMRDFASpecificaiton(query.Filter, query.IdObra), cancellationToken);
            if (items.Count == 0) return Result<PagedResult<GetLVMRDFAVM>>.Failure(new Error("404", "No result found."));

            var result = items.GroupBy(c => c.MascaraLVM)
                        .Select(g => new GetLVMRDFAVM
                        {
                            IdLVM = g.Min(x => x.Id),
                            NumeroLVM = g.Key,
                            NotaFiscal = string.Join(", ", g.Select(x => x.NotaFiscal))
                        }).ToList();

            var predicate = GetLVMRDFASpecificaiton.BuildFilter(query.Filter, query.IdObra);
            var totalCount = await _repos.CountAsync(new CountSpecification<CadLVM>(predicate), cancellationToken);

            return Result<PagedResult<GetLVMRDFAVM>>.Success(new PagedResult<GetLVMRDFAVM>
            {
                Items = result,
                Page = query.PageNo,
                TotalCount = totalCount
            });
        }
    }
}
