using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadMaterial;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Materials.Commands.Create
{
    [Authorize]
    public record CreateMaterial : MaterialDTO, IRequest<Result<int>>, IIdempotentRequest
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateMaterialCommandHandler(IRepositoryCad<EntityMaterial> repos) : IRequestHandler<CreateMaterial, Result<int>>
    {
        private readonly IRepositoryCad<EntityMaterial> _repository = repos;
        public async Task<Result<int>> Handle(CreateMaterial command, CancellationToken cancellationToken)
        {
            var material = EntityMaterial.Create(command.Material);

            await _repository.AddAsync(material, cancellationToken);

            return Result<int>.Success(material.Id, "Transportadora Successfully inserted.");
        }
    }

}
