using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadObra;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Obras.Commands.Update
{
    [Authorize]
    public record UpdateObra : ObraDTO, IRequest<Result<int>>, IIdempotentRequest
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateObraCommandHandler(IRepositoryCad<EntityObra> repos) : IRequestHandler<UpdateObra, Result<int>>
    {
        private readonly IRepositoryCad<EntityObra> _repository = repos;
        public async Task<Result<int>> Handle(UpdateObra command, CancellationToken cancellationToken)
        {
            var obra = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (obra is null) return Result<int>.Failure(new Error("404", $"Fornecedor Id:{command.Id} not found"));

            obra.Update(command.Obra, command.Cliente, command.Descricao, command.Contrato, command.Mascara, command.Ultlvm, command.TransferenciaValue);

            await _repository.UpdateAsync(obra, cancellationToken);

            return Result<int>.Success(obra.Id, "Obra Successfully updated.");
        }
    }

}
