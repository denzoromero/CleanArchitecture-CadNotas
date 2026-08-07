using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Create
{
    public record CreateCLVM : CLVMDTO, IIdempotentRequest, IRequest<Result<int>>
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateCLVMCommandHandler(IRepositoryCad<CadClvm> repos, IUser user) : IRequestHandler<CreateCLVM, Result<int>>
    {
        private readonly IRepositoryCad<CadClvm> _repository = repos;
        private readonly IUser _user = user;
        public async Task<Result<int>> Handle(CreateCLVM command, CancellationToken cancellationToken)
        {
            var IsDuplicated = await _repository.AnyAsync(new DuplicateCLVMSpecification(command.IdLVM, command.Item), cancellationToken);
            if (IsDuplicated) return Result<int>.Failure(new Error("403", "Item is Duplicated"));

            var entity = CadClvm.Create(command.IdLVM, command.Item, command.Codigo, command.DtInspecao, command.Status, command.PO, command.IdMaterialCLVM,
                command.CodObra, command.TipoComponente, command.Certificacao, command.Corrida, command.TMA, command.LVMOrigem, command.UnidadeMedida,
                command.Qtd, _user.UserId, command.PO, command.Observacao);

            await _repository.AddAsync(entity, cancellationToken);

            return Result<int>.Success(entity.Id, "CLVM successfully added.");
        }
    }
}
