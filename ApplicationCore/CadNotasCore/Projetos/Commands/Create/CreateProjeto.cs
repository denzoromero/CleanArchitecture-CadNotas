using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadProjeto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Projetos.Commands.Create
{
    [Authorize]
    public record CreateProjeto : ProjetoDTO, IRequest<Result<int>>, IIdempotentRequest
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateProjetoCommandHandler(IRepositoryCad<CadProjeto> repos) : IRequestHandler<CreateProjeto, Result<int>>
    {
        private readonly IRepositoryCad<CadProjeto> _repository = repos;
        public async Task<Result<int>> Handle(CreateProjeto command, CancellationToken cancellationToken)
        {
            var projeto = CadProjeto.Create(command.Projeto, command.IdObras);

            await _repository.AddAsync(projeto, cancellationToken);

            return Result<int>.Success(projeto.Id, "Projeto Successfully inserted.");
        }
    }

}
