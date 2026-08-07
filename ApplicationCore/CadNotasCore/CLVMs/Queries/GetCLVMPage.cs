using ApplicationCore.CadNotasCore.CLVMs.Specifications;
using ApplicationCore.CadNotasCore.CLVMs.VMs;
using ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries.VMs;
using ApplicationCore.CadNotasCore.NotaFiscaisCore.Specifications;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Queries
{
    public record GetCLVMPage(int id) : IRequest<Result<CLVMPageVM>>;

    public class GetCLVMPageQueryHandler(IRepositoryCad<CadClvm> repos, IRepositoryCad<CadLVM> reposLVM, IRepositoryCad<CadProcedimento> reposProc) : IRequestHandler<GetCLVMPage, Result<CLVMPageVM>>
    {
        private readonly IRepositoryCad<CadClvm> _repository = repos;
        private readonly IRepositoryCad<CadLVM> _repositoryLVM = reposLVM;
        private readonly IRepositoryCad<CadProcedimento> _repositoryProc = reposProc;
        public async Task<Result<CLVMPageVM>> Handle(GetCLVMPage request, CancellationToken cancellationToken)
        {
            var lvmSample = await _repositoryLVM.GetByIdAsync(request.id, cancellationToken);

            var lvm = await _repositoryLVM.FirstOrDefaultAsync(new GetLVMByIdSpecification(request.id), cancellationToken);
            if (lvm is null) return Result<CLVMPageVM>.Failure(new Error("ERR404", "No result found."));

            //var items = await _repository.ListAsync(new CLVMPageSpecification(request.id), cancellationToken);
            //if (items.Count == 0) return Result<CLVMPageVM>.Failure(new Error("ERR404", "No result found."));

            var procedimentos = await _repositoryProc.ListAsync(new GetProcedimentosSpecificaiton(), cancellationToken);

            return Result<CLVMPageVM>.Success(new CLVMPageVM
            {
                Nota = lvm,
                Procedimentos = procedimentos,
                CLVMs = lvm.Materials
            });

        }

    }
}
