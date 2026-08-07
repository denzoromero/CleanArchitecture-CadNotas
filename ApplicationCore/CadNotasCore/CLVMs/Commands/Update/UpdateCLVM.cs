using ApplicationCore.CadNotasCore.CLVMs.Commands.Create;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Update
{
    public record UpdateCLVM : CLVMDTO, IIdempotentRequest, IEntityRequest, IRequest<Result<int>>
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateCLVMCommandHandler(IRepositoryCad<CadClvm> repos) : IRequestHandler<UpdateCLVM, Result<int>>
    {
        private readonly IRepositoryCad<CadClvm> _repository = repos;
        public async Task<Result<int>> Handle(UpdateCLVM req, CancellationToken cancellationToken)
        {
            var IsDuplicated = await _repository.AnyAsync(new DuplicateCLVMSpecification(req.IdLVM, req.Item, req.Id), cancellationToken);
            if (IsDuplicated) return Result<int>.Failure(new Error("403", "Item is Duplicated"));

            var clvm = await _repository.GetByIdAsync(req.Id, cancellationToken);
            if (clvm is null) return Result<int>.Failure(new Error("404", "Entity not found."));

            clvm.Update(req.IdLVM, req.Item, req.Codigo, req.DtInspecao, req.Status, req.PO, req.IdMaterialCLVM,
                req.CodObra, req.TipoComponente, req.Certificacao, req.Corrida, req.TMA, req.LVMOrigem, req.UnidadeMedida,
                req.Qtd, req.PO, req.Observacao);

            await _repository.UpdateAsync(clvm, cancellationToken);
            return Result<int>.Success(clvm.Id, "CLVM successfully edited.");
        }

    }

}
