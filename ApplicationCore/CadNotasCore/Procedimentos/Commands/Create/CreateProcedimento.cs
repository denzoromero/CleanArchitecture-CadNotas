using ApplicationCore.CadNotasCore.MaterialCLVMs.Commands.Create;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Procedimentos.Commands.Create
{
    public record CreateProcedimento : ProcedimentoDTO, IIdempotentRequest, IRequest<Result<int>>
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateProcedimentoCommandHandler(IRepositoryCad<CadProcedimento> repos) : IRequestHandler<CreateProcedimento, Result<int>>
    {
        private readonly IRepositoryCad<CadProcedimento> _repository = repos;

        public async Task<Result<int>> Handle(CreateProcedimento command, CancellationToken cancellationToken)
        {
            var procedimento = CadProcedimento.Create(command.Procedimento, command.IdObra, command.Revisao);

            await _repository.AddAsync(procedimento, cancellationToken);

            return Result<int>.Success(procedimento.Id, "Procedimento successfully added.");
        }
    }

}
