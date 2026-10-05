using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Disciplinas.Commands.Create
{
    public record CreateDisciplina(string Disciplina) : DisciplinaDTO(Disciplina), IIdempotentRequest, IRequest<Result<int>>
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateDisciplinaCommandHandler(IRepositoryCad<EntityDisciplina> repos) : IRequestHandler<CreateDisciplina, Result<int>>
    {
        private readonly IRepositoryCad<EntityDisciplina> _repository = repos;
        public async Task<Result<int>> Handle(CreateDisciplina command, CancellationToken cancellationToken)
        {
            var disciplina = EntityDisciplina.Create(command.Disciplina);

            await _repository.AddAsync(disciplina, cancellationToken);

            return Result<int>.Success(disciplina.Id, "Disciplina successfully added.");
        }
    }

}
