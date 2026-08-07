using ApplicationCore.CadNotasCore.Procedimentos.Commands.Update;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Command.Update
{
    public record UpdateNotaFiscais : NotaFiscaisDTO, IIdempotentRequest, IEntityRequest, IRequest<Result<int>>
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateNotaFiscaisCommandHandler(IRepositoryCad<CadLVM> repos) : IRequestHandler<UpdateNotaFiscais, Result<int>>
    {
        private readonly IRepositoryCad<CadLVM> _repository = repos;

        public async Task<Result<int>> Handle(UpdateNotaFiscais command, CancellationToken cancellationToken)
        {
            var lvm = await _repository.GetByIdAsync(command.Id, cancellationToken);
            if (lvm is null) return Result<int>.Failure(new Error("404", "Entity not found."));

            lvm.Update(command.IdObra, command.Data,
                command.NLVM,
                command.MascaraLVM,
                command.NotaFiscal,
                command.Documento,
                command.IdFornecedor,
                command.OC,
                command.RM,
                command.RC,
                command.IdMaterial,
                command.IdDisciplina,
                command.IdTipoOC,
                command.Valor,
                command.Obs);

            await _repository.UpdateAsync(lvm, cancellationToken);
            return Result<int>.Success(lvm.Id, "NotaFiscais successfully edited.");
        }
    }

}
