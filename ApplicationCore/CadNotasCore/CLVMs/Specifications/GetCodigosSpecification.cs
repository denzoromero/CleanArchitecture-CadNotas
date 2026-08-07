using ApplicationCore.CadNotasCore.MaterialCLVMs.Queries;
using Ardalis.Specification;
using Domain.Entities.EntitiesCad;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Specifications
{
    public class GetCodigosSpecification : Specification<CadMaterialCLVM, MaterialCLVMVM>
    {
        public GetCodigosSpecification(string codigo) 
        {
            Query.AsNoTracking().Where(i => i.Codigo.Contains(codigo, StringComparison.CurrentCultureIgnoreCase) && i.Ativo == 1)
                .Select(i => new MaterialCLVMVM
                {
                    Id = i.Id,
                    Codigo = i.Codigo,
                    TipoComponenteMaterial = i.TipoComponenteMaterial,
                    Especificacao = i.Especificacao,
                    Diametro1 = i.Diametro1,
                    Diametro2 = i.Diametro2,
                    Comprimento = i.Comprimento,
                    Espessura = i.Espessura,
                    Largura = i.Largura,
                    Peso = i.Peso,
                });
        }
    }
}
