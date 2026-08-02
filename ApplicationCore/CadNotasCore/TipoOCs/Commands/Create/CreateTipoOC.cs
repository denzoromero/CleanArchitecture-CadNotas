using ApplicationCore.CadNotasCore.Disciplinas;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.TipoOCs.Commands.Create
{
    public record CreateTipoOC(string Nome) : IIdempotentRequest, IRequest<Result<int>>
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateTipoOCCommandHandler(IRepositoryCad<CadTipoOC> repos) : IRequestHandler<CreateTipoOC, Result<int>>
    {
        private readonly IRepositoryCad<CadTipoOC> _repository = repos;
        public async Task<Result<int>> Handle(CreateTipoOC command, CancellationToken cancellationToken)
        {
            var tipooc = CadTipoOC.Create(command.Nome);

            await _repository.AddAsync(tipooc, cancellationToken);

            return Result<int>.Success(tipooc.Id, "Tipo OC successfully added.");
        }
    }
}
