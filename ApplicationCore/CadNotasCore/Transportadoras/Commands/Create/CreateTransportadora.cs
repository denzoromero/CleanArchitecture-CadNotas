using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadTransportadora;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Transportadoras.Commands.Create
{
    [Authorize]
    public record CreateTransportadora : TransportadoraDTO, IRequest<Result<int>>, IIdempotentRequest
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateTransportadoraCommandHandler(IRepositoryCad<CadTransportadora> repos, IUser user) : IRequestHandler<CreateTransportadora, Result<int>>
    {
        private readonly IRepositoryCad<CadTransportadora> _repository = repos;
        private readonly IUser _user = user;

        public async Task<Result<int>> Handle(CreateTransportadora request, CancellationToken cancellationToken)
        {

            var transportadora = CadTransportadora.Create(new TransportadoraPO(request.Nome,
                request.IE,
                request.CNPJ,
                request.ConhecTransp,
                request.Rua,
                request.Numero,
                request.Bairro,
                request.IdCidade,
                request.IdEstado,
                request.Cep,
                request.Telefone1,
                request.Telefone2,
                request.Fax,
                request.EMail,
                request.HomePage));

            await _repository.AddAsync(transportadora, cancellationToken);

            return Result<int>.Success(transportadora.Id, "Transportadora Successfully inserted.");
        }

    }

}
