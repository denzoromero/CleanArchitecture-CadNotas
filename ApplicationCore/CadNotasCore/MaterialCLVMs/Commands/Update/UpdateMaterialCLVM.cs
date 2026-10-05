using ApplicationCore.CadNotasCore.TipoOCs;
using ApplicationCore.CadNotasCore.TipoOCs.Commands.Update;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.MaterialCLVMs.Commands.Update
{
    public record UpdateMaterialCLVM : MaterialCLVMDTO, IIdempotentRequest, IEntityRequest, IRequest<Result<int>>
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateMaterialCLVMCommandHandler(IRepositoryCad<EntityMaterialCLVM> repos) : IRequestHandler<UpdateMaterialCLVM, Result<int>>
    {
        private readonly IRepositoryCad<EntityMaterialCLVM> _repository = repos;
        public async Task<Result<int>> Handle(UpdateMaterialCLVM command, CancellationToken cancellationToken)
        {
            var materialCLVM = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (materialCLVM is null) return Result<int>.Failure(new Error("404", "Entity not found."));

            materialCLVM.Update(command.Codigo, command.Especificacao, command.Descricao, command.Diametro1, command.Diametro2, command.Comprimento, command.Espessura,
                command.Largura, command.Peso, command.TipoComponenteMaterial);

            await _repository.UpdateAsync(materialCLVM, cancellationToken);
            return Result<int>.Success(materialCLVM.Id, "MaterialCLVM successfully edited.");

        }

    }

}
