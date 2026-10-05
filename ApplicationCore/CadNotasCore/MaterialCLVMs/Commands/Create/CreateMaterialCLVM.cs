using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;

namespace ApplicationCore.CadNotasCore.MaterialCLVMs.Commands.Create
{
    [Authorize]
    public record CreateMaterialCLVM : MaterialCLVMDTO, IIdempotentRequest, IRequest<Result<int>>
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateMaterialCLVMCommandHandler(IRepositoryCad<EntityMaterialCLVM> repos) : IRequestHandler<CreateMaterialCLVM, Result<int>>
    {
        private readonly IRepositoryCad<EntityMaterialCLVM> _repository = repos;

        public async Task<Result<int>> Handle(CreateMaterialCLVM command, CancellationToken cancellationToken)
        {
            var materialCLVM = EntityMaterialCLVM.Create(command.Codigo, command.Especificacao, command.Descricao, command.Diametro1,
                command.Diametro2, command.Comprimento, command.Espessura, command.Largura, command.Peso, command.TipoComponenteMaterial);

            await _repository.AddAsync(materialCLVM, cancellationToken);

            return Result<int>.Success(materialCLVM.Id, "MaterialCLVM successfully added.");
        }

    }




}
