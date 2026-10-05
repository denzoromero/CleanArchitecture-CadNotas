using ApplicationCore.CadNotasCore.MaterialCLVMs.Queries;
using ApplicationCore.CadNotasCore.Materials.Queries;
using Ardalis.Specification;
using Domain;
using Domain.Entities.EntitiesCad;
using Domain.Entities.EntitiesCad.ECadMaterial;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.CadNotasCore.MaterialCLVMs.Specifications
{
    public class CadMaterialCLVMSpecification : Specification<EntityMaterialCLVM, MaterialCLVMVM>
    {
        public static Expression<Func<EntityMaterialCLVM, bool>> BuildFilter(string? filter, int ativo)
        {
            return i => (string.IsNullOrEmpty(filter)
                            || i.Codigo.Contains(filter)
                            || (i.Descricao != null && i.Descricao.Contains(filter))
                            || (i.Especificacao != null && i.Especificacao.Contains(filter)))
                            && i.Ativo == ativo;
        }

        public CadMaterialCLVMSpecification(string? filter, int ativo, int pageNo = 0)
        {
            Query.AsNoTracking().Where(BuildFilter(filter, ativo))
                           .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                           .Take(Constants.ITEMS_PER_PAGE)
                           .Select(i => new MaterialCLVMVM
                           {
                               Id = i.Id,
                               Codigo = i.Codigo,
                               Especificacao = i.Especificacao,
                               Descricao = i.Descricao,
                               Diametro1 = i.Diametro1,
                               Diametro2 = i.Diametro2,
                               Comprimento = i.Comprimento,
                               Espessura = i.Espessura,
                               Largura = i.Largura,
                               Peso = i.Peso,
                               TipoComponenteMaterial = i.TipoComponenteMaterial
                           });
        }

    }
}
