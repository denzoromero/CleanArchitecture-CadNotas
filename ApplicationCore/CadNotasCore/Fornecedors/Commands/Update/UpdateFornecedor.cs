using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using MediatR;

namespace ApplicationCore.CadNotasCore.Fornecedors.Commands.Update
{
    [Authorize]
    public record UpdateFornecedor : CommonFornecedorDTO, IRequest<Result<int>>, IIdempotentRequest
    {
        public int Id { get; init; }
        public Guid IdempotencyKey { get; init; }
    }

    public class UpdateFornecedorCommandHandler(IRepositoryCad<EntityFornecedor> repos, IUser user) : IRequestHandler<UpdateFornecedor, Result<int>>
    {
        private readonly IRepositoryCad<EntityFornecedor> _repository = repos;
        private readonly IUser _user = user;

        public async Task<Result<int>> Handle(UpdateFornecedor req, CancellationToken cancellationToken)
        {

            var fornecedor = await _repository.GetByIdAsync(req.Id, cancellationToken);
            if (fornecedor is null) return Result<int>.Failure(new Error("404", $"Fornecedor Id:{req.Id} not found"));

            fornecedor.Update(new FornecedorPO(
                req.Fornecedor,
                req.Fantasia,
                req.Contato,
                req.IE,
                req.CNPJ,
                req.Rua,
                req.Numero,
                req.Bairro,
                req.IdCidade,
                req.IdEstado,
                req.Cep,
                req.Telefone1,
                req.Telefone2,
                req.EMail,
                req.HomePage,
                _user.UserId
                ));

            await _repository.UpdateAsync(fornecedor, cancellationToken);

            return Result<int>.Success(fornecedor.Id, "Fornecedor Successfully updated.");
        }
    }


}
