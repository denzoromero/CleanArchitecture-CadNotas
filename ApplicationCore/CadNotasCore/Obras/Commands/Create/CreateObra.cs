using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadObra;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Obras.Commands.Create
{
    [Authorize]
    public record CreateObra : ObraDTO, IRequest<Result<int>>, IIdempotentRequest
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateObraCommandHanlder(IRepositoryCad<CadObra> repos) : IRequestHandler<CreateObra, Result<int>>
    {
        private readonly IRepositoryCad<CadObra> _repository = repos;
        public async Task<Result<int>> Handle(CreateObra command, CancellationToken cancellationToken)
        {
            var obra = CadObra.Create(command.Obra, command.Cliente, command.Descricao, command.Contrato, command.Mascara, command.Ultlvm, command.TransferenciaValue);

            await _repository.AddAsync(obra, cancellationToken);

            return Result<int>.Success(obra.Id, "Obra Successfully inserted.");
        }
    }

}
