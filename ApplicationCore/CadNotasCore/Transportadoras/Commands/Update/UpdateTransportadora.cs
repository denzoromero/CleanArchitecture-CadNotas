using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadTransportadora;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Transportadoras.Commands.Update
{
    [Authorize]
    public record UpdateTransportadora : TransportadoraDTO, IRequest<Result<int>>, IIdempotentRequest
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateTransportadoraCommandHandler(IRepositoryCad<EntityTransportadora> repos) : IRequestHandler<UpdateTransportadora , Result<int>>
    {
        private readonly IRepositoryCad<EntityTransportadora> _repository = repos;

        public async Task<Result<int>> Handle(UpdateTransportadora req, CancellationToken cancellationToken)
        {
            var transportadora = await _repository.GetByIdAsync(req.Id, cancellationToken);
            if (transportadora is null) return Result<int>.Failure(new Error("404", $"Transportadora Id:{req.Id} not found"));

            transportadora.Update(new TransportadoraPO(
                req.Nome,
                req.IE,
                req.CNPJ,
                req.ConhecTransp,
                req.Rua,
                req.Numero,
                req.Bairro,
                req.IdCidade,
                req.IdEstado,
                req.Cep,
                req.Telefone1,
                req.Telefone2,
                req.Fax,
                req.EMail,
                req.HomePage));

            await _repository.UpdateAsync(transportadora, cancellationToken);

            return Result<int>.Success(transportadora.Id, "Transportadora Successfully updated.");

        }
    }
}
