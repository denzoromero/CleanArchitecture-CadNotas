using ApplicationCore.BSCore.GetUser;
using ApplicationCore.CadNotasCore.RelatorioCLVMs.Queries.Specificaiton;
using ApplicationCore.CadNotasCore.RelatorioCLVMs.Queries.VM;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesBS;
using Domain.Entities.EntitiesCad;
using MediatR;

namespace ApplicationCore.CadNotasCore.RelatorioCLVMs.Queries
{
    public record GetPrintCLVMPage(int IdLVM) : IRequest<GetPrintCLVMPageVM?>;

    public class GetPrintCLVMPageQueryHandler(IRepositoryCad<EntityLVM> repos, IRepositoryBS<UsuarioBS> reposBS) : IRequestHandler<GetPrintCLVMPage, GetPrintCLVMPageVM?>
    {
        private readonly IRepositoryCad<EntityLVM> _repos = repos;
        private readonly IRepositoryBS<UsuarioBS> _reposBS = reposBS;
        public async Task<GetPrintCLVMPageVM?> Handle(GetPrintCLVMPage req, CancellationToken cancellationToken)
        {
            var report = await _repos.FirstOrDefaultAsync(new GetPrintCLVMSpecification(req.IdLVM), cancellationToken);
            if (report is not null)
            {
                report.InspetorName = report.IdInspetor != null ? await _reposBS.FirstOrDefaultAsync(new GetUserInformationSpecification(report.IdInspetor.Value), cancellationToken)
                    : null;

                report.VerificadorName = report.IdVerificador != null ? await _reposBS.FirstOrDefaultAsync(new GetUserInformationSpecification(report.IdVerificador.Value), cancellationToken)
                    : null;
            }

            return report;
        }
    }
}
