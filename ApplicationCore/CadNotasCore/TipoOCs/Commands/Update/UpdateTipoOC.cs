using ApplicationCore.CadNotasCore.Disciplinas;
using ApplicationCore.CadNotasCore.Disciplinas.Commands.Update;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.TipoOCs.Commands.Update
{
    public record UpdateTipoOC(string Nome) : IIdempotentRequest, IEntityRequest, IRequest<Result<int>>
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateTipoOCCommandHandler(IRepositoryCad<CadTipoOC> repos) : IRequestHandler<UpdateTipoOC, Result<int>>
    {
        private readonly IRepositoryCad<CadTipoOC> _repository = repos;
        public async Task<Result<int>> Handle(UpdateTipoOC command, CancellationToken cancellationToken)
        {
            var tipooc = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (tipooc is null) return Result<int>.Failure(new Error("404", "Entity not found."));

            tipooc.Update(command.Nome);

            await _repository.UpdateAsync(tipooc, cancellationToken);
            return Result<int>.Success(tipooc.Id, "TipoOC successfully edited.");
        }
    }

}
