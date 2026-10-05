using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Disciplinas.Commands.Update
{
    public record UpdateDisciplina(string Disciplina) : DisciplinaDTO(Disciplina), IIdempotentRequest, IRequest<Result<int>>
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateDisciplinaCommandHandler(IRepositoryCad<EntityDisciplina> repos) : IRequestHandler<UpdateDisciplina, Result<int>>
    {
        private readonly IRepositoryCad<EntityDisciplina> _repository = repos;
        public async Task<Result<int>> Handle(UpdateDisciplina command, CancellationToken cancellationToken)
        {
            var disciplina = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (disciplina is null) return Result<int>.Failure(new Error("404", "Entity not found."));

            disciplina.Update(command.Disciplina);

            await _repository.UpdateAsync(disciplina, cancellationToken);
            return Result<int>.Success(disciplina.Id, "Disciplina successfully edited.");

        }
    }

}
