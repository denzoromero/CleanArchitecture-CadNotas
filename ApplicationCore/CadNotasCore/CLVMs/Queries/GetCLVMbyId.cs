using ApplicationCore.CadNotasCore.CLVMs.Specifications;
using ApplicationCore.CadNotasCore.CLVMs.VMs;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Queries
{
    public record GetCLVMbyId(int id) : IRequest<Result<CLVMbyIdVM>>;

    public class GetCLVMbyIdQueryHandler (IRepositoryCad<EntityClvm> repos) : IRequestHandler<GetCLVMbyId, Result<CLVMbyIdVM>>
    {
        private readonly IRepositoryCad<EntityClvm> _repository = repos;
        public async Task<Result<CLVMbyIdVM>> Handle(GetCLVMbyId req, CancellationToken cancellationToken)
        {
            var item = await _repository.FirstOrDefaultAsync(new GetCLVMbyIdSpecification(req.id), cancellationToken);
            if (item is null) return Result<CLVMbyIdVM>.Failure(new Error("404", "no entity found"));

            return Result<CLVMbyIdVM>.Success(item);
        }
    }
}
