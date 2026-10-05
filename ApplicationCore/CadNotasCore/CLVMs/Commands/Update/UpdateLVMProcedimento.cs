using ApplicationCore.CadNotasCore.Procedimentos.Commands.Update;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Update
{
    public record UpdateLVMProcedimento(int IdProcedimento) : IIdempotentRequest, IEntityRequest, IRequest<Result<int>>
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateLVMProcedimentoCommandHandler(IRepositoryCad<EntityLVM> repos) : IRequestHandler<UpdateLVMProcedimento, Result<int>>
    {
        private readonly IRepositoryCad<EntityLVM> _repository = repos;

        public async Task<Result<int>> Handle(UpdateLVMProcedimento command, CancellationToken cancellationToken)
        {
            var lvm = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (lvm is null) return Result<int>.Failure(new Error("404", "Entity not found."));

            lvm.UpdateLVMProcedimento(command.IdProcedimento);

            await _repository.UpdateAsync(lvm, cancellationToken);
            return Result<int>.Success(lvm.Id, "LVM Procedimento successfully edited.");
        }
    }


}
