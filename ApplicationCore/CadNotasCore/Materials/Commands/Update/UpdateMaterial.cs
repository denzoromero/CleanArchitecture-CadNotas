using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadMaterial;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Materials.Commands.Update
{
    [Authorize]
    public record UpdateMaterial : MaterialDTO, IRequest<Result<int>>, IIdempotentRequest
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateMaterialCommandHandler(IRepositoryCad<CadMaterial> repos) : IRequestHandler<UpdateMaterial, Result<int>>
    {
        private readonly IRepositoryCad<CadMaterial> _repository = repos;
        public async Task<Result<int>> Handle(UpdateMaterial command, CancellationToken cancellationToken)
        {
            var material = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (material is null) return Result<int>.Failure(new Error("404", $"Transportadora Id:{command.Id} not found"));

            material.Update(command.Material);

            await _repository.UpdateAsync(material, cancellationToken);

            return Result<int>.Success(material.Id, "Material Successfully updated.");

        }
    }

}
