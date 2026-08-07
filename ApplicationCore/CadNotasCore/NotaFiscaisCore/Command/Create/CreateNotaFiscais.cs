using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Command.Create
{
    public record CreateNotaFiscais : NotaFiscaisDTO, IIdempotentRequest, IRequest<Result<int>>
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateNotaFiscaisCommandHandler(IRepositoryCad<CadLVM> repos) : IRequestHandler<CreateNotaFiscais, Result<int>>
    {
        private readonly IRepositoryCad<CadLVM> _repository = repos;

        public async Task<Result<int>> Handle(CreateNotaFiscais command, CancellationToken cancellationToken)
        {
            var lvm = CadLVM.Create(command.IdObra, command.Data,
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

            await _repository.AddAsync(lvm, cancellationToken);

            return Result<int>.Success(lvm.Id, "LVM successfully added.");
        }
    }

}
