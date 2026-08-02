using ApplicationCore.CadNotasCore.MaterialCLVMs.Specifications;
using ApplicationCore.CadNotasCore.Materials.Queries;
using ApplicationCore.CadNotasCore.Materials.Specifications;
using ApplicationCore.Common;
using ApplicationCore.Common.DTO;
using ApplicationCore.Common.Specifications;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using Domain.Entities.EntitiesCad.ECadMaterial;
using MediatR;

namespace ApplicationCore.CadNotasCore.MaterialCLVMs.Queries
{
    public record SearchMaterialCLVM : SearchDTO, IRequest<Result<PagedResult<MaterialCLVMVM>>>;

    public class SearchMaterialCLVMQueryHandler(IRepositoryCad<CadMaterialCLVM> repos) : IRequestHandler<SearchMaterialCLVM, Result<PagedResult<MaterialCLVMVM>>>
    {
        private readonly IRepositoryCad<CadMaterialCLVM> _repository = repos;

        public async Task<Result<PagedResult<MaterialCLVMVM>>> Handle(SearchMaterialCLVM query, CancellationToken cancellationToken)
        {
            var items = await _repository.ListAsync(new CadMaterialCLVMSpecification(query.Filter, query.AtivoValue, query.PageNo));
            if (items.Count == 0) return Result<PagedResult<MaterialCLVMVM>>.Failure(new Error("ERR404", "No result found."));

            var predicate = CadMaterialCLVMSpecification.BuildFilter(query.Filter, query.AtivoValue);
            var totalCount = await _repository.CountAsync(new CountSpecification<CadMaterialCLVM>(predicate));

            return Result<PagedResult<MaterialCLVMVM>>.Success(new PagedResult<MaterialCLVMVM>
            {
                Items = items,
                Page = query.PageNo,
                TotalCount = totalCount
            });

        }
    }

}
