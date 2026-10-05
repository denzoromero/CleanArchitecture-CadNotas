using ApplicationCore.CadNotasCore.Projetos.Specifications;
using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadProjeto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace ApplicationCore.CadNotasCore.Projetos.Commands.Update
{
    [Authorize]
    public record UpdateProjeto : ProjetoDTO, IRequest<Result<int>>, IIdempotentRequest
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateProjetoCommandHandler(IRepositoryCad<EntityProjeto> repos) : IRequestHandler<UpdateProjeto, Result<int>>
    {
        private readonly IRepositoryCad<EntityProjeto> _repository = repos;
        public async Task<Result<int>> Handle(UpdateProjeto command, CancellationToken cancellationToken)
        {
            var projeto = await _repository.FirstOrDefaultAsync(new ProjetoById(command.Id), cancellationToken);
            if (projeto is null) return Result<int>.Failure(new Error("404", "Entity not found."));

            projeto.Update(command.Projeto, command.IdObras);

            await _repository.UpdateAsync(projeto, cancellationToken);

            return Result<int>.Success(projeto.Id, "Transportadora Successfully updated.");
        }
    }

}
