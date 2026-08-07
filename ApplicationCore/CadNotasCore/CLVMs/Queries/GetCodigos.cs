using ApplicationCore.CadNotasCore.CLVMs.Specifications;
using ApplicationCore.CadNotasCore.MaterialCLVMs.Queries;
using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Queries
{
    public record GetCodigos(string codigo) : IRequest<Result<List<MaterialCLVMVM>?>>;

    public class GetCodigosQueryHandler(IRepositoryCad<CadMaterialCLVM> repos) : IRequestHandler<GetCodigos, Result<List<MaterialCLVMVM>?>>
    {
        private readonly IRepositoryCad<CadMaterialCLVM> _repository = repos;
        public async Task<Result<List<MaterialCLVMVM>?>> Handle(GetCodigos request, CancellationToken cancellationToken)
        {
            var materials = await _repository.ListAsync(new GetCodigosSpecification(request.codigo), cancellationToken);

            return Result<List<MaterialCLVMVM>?>.Success(materials);
        }
    }
}
