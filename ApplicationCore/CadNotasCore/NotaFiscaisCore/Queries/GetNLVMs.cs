using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries
{
    public record GetNLVMs(int IdObra) : IRequest<Result<List<string>>>;

    public class GetNLVMsQueryHandler(IContextCad context) : IRequestHandler<GetNLVMs, Result<List<string>>>
    {
        private readonly IContextCad _context = context;
        public async Task<Result<List<string>>> Handle(GetNLVMs query, CancellationToken cancellationToken)
        {
            var ddlList = await _context.CadLVMs.Where(i => i.IdObra == query.IdObra && i.NLVM != null && i.NLVM != "SLVM")
                                       .Select(i => i.NLVM ?? string.Empty)
                                           .Distinct()
                                              .OrderByDescending(nlvm => nlvm)
                                       .ToListAsync(cancellationToken);

            if (ddlList.Count == 0) return Result<List<string>>.Failure(new Error("404", "No LVMs found."));


            return Result<List<string>>.Success(ddlList);

        }
    }


}
