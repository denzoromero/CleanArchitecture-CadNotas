using ApplicationCore.Common;
using ApplicationCore.Common.Security;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using MediatR;

namespace ApplicationCore.CadNotasCore.Fornecedors.Commands.Create
{
    [Authorize]
    public record CreateFornecedor : CommonFornecedorDTO, IRequest<Result<int>>, IIdempotentRequest
    {
        public Guid IdempotencyKey { get; init; }
    }

    public class CreateFornecedorCommandHandler(IRepositoryCad<EntityFornecedor> repos, IUser user) : IRequestHandler<CreateFornecedor, Result<int>>
    {
        private readonly IRepositoryCad<EntityFornecedor> _repository = repos;
        private readonly IUser _user = user;

        public async Task<Result<int>> Handle(CreateFornecedor req, CancellationToken cancellationToken)
        {

            var fornecedor = EntityFornecedor.Create(new FornecedorPO(
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
                _user.UserId));

            await _repository.AddAsync(fornecedor, cancellationToken);
            //await _repository.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(fornecedor.Id,"Fornecedor Successfully inserted.");
        }
    }



}
