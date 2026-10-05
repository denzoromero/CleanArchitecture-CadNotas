using ApplicationCore.CadNotasCore.CLVMs.Specifications;
using ApplicationCore.CadNotasCore.CLVMs.VMs;
using ApplicationCore.CadNotasCore.Procedimentos.Queries;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;

namespace ApplicationCore.CadNotasCore.RDFAs.Queries.GetRDFAPage
{
    public record GetRDFAPageQuery(int id) : IRequest<Result<GetRDFAPageVM>>;
    
    public class GetRDFAPageQueryHandler(IRepositoryCad<EntityLVM> reposLVM) : IRequestHandler<GetRDFAPageQuery, Result<GetRDFAPageVM>>
    {
        private readonly IRepositoryCad<EntityLVM> _repositoryLVM = reposLVM;
        public async Task<Result<GetRDFAPageVM>> Handle(GetRDFAPageQuery request, CancellationToken cancellationToken)
        {
            var lvm = await _repositoryLVM.FirstOrDefaultAsync(new GetLVMByIdSpecification(request.id), cancellationToken);
            if (lvm is null) return Result<GetRDFAPageVM>.Failure(new Error("ERR404", "No result found."));

            return Result<GetRDFAPageVM>.Success(new GetRDFAPageVM
            {
                Nota = lvm,
                CLVMs = lvm.Materials
            });
        }
    }
}
