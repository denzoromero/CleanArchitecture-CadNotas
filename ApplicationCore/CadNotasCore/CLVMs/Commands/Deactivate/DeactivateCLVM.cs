using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Deactivate
{
    public record DeactivateCLVM(int Id, int IdLVM) : IRequest<Result<int>>;

    public class DeactivateCLVMCommandHandler(IRepositoryCad<CadClvm> repos) : IRequestHandler<DeactivateCLVM, Result<int>>
    {
        private readonly IRepositoryCad<CadClvm> _repository = repos;
        public async Task<Result<int>> Handle(DeactivateCLVM req, CancellationToken cancellationToken)
        {
            var clvm = await _repository.GetByIdAsync(req.Id, cancellationToken);
            if (clvm is null) return Result<int>.Failure(new Error("404", "Entity not found."));

            clvm.Deactivate();

            await _repository.UpdateAsync(clvm, cancellationToken);
            return Result<int>.Success(clvm.Id, "CLVM successfully edited.");
        }
    }

}
