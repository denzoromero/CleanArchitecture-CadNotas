using ApplicationCore.CadNotasCore.Procedimentos.Commands.Create;
using ApplicationCore.CadNotasCore.TipoOCs;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Procedimentos.Commands.Update
{
    public record UpdateProcedimento : ProcedimentoDTO, IIdempotentRequest, IEntityRequest, IRequest<Result<int>>
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateProcedimentoCommandHandler(IRepositoryCad<CadProcedimento> repos) : IRequestHandler<UpdateProcedimento, Result<int>>
    {
        private readonly IRepositoryCad<CadProcedimento> _repository = repos;

        public async Task<Result<int>> Handle(UpdateProcedimento command, CancellationToken cancellationToken)
        {
            var procedimento = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (procedimento is null) return Result<int>.Failure(new Error("404", "Entity not found."));

            procedimento.Update(command.Procedimento,command.IdObra,command.Revisao);

            await _repository.UpdateAsync(procedimento, cancellationToken);
            return Result<int>.Success(procedimento.Id, "Procedimento successfully edited.");
        }
    }


}
