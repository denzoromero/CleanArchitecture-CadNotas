using ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries.VMs;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries
{
    public record MakeMascara(int IdObra) : IRequest<Result<MakeMascaraVM>>;

    public class MakeMascaraQueryHandler(IContextCad context) : IRequestHandler<MakeMascara, Result<MakeMascaraVM>>
    {
        private readonly IContextCad _context = context;
        public async Task<Result<MakeMascaraVM>> Handle(MakeMascara query, CancellationToken cancellationToken)
        {
            //var lvm = await _context.CadLVMs.Where(x => x.IdObra == query.IdObra && x.NLVM != "SLVM").MaxAsync(cancellationToken);

            //string nextNlvm = ((int?)Convert.ToInt32(lvm.NLVM) ?? 0 + 1).ToString("D4");
            //string mascaralvm = $"{lvm.Obra.Obra}-{nextNlvm}";

            int lastNlvm = await _context.CadLVMs
                            .Select(x => (int?)Convert.ToInt32(x.NLVM)).MaxAsync(cancellationToken) ?? 0;

            string nextNlvm = (lastNlvm + 1).ToString("D4");

            string? obraName = await _context.CadObras.Where(x => x.Id == query.IdObra).Select(x => x.Obra).FirstOrDefaultAsync(cancellationToken);

            string mascara = $"{obraName}-{nextNlvm}";

            //var result = await _context.CadLVMs.Where(i => i.IdObra == query.IdObra )
            //                .Select(i => new MakeMascaraVM(i.NLVM,i.MascaraLVM)).MaxAsync();


            return Result<MakeMascaraVM>.Success(new MakeMascaraVM(nextNlvm, mascara));
        }
    }

}
