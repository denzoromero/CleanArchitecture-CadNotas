using ApplicationCore.CadNotasCore.CLVMs.VMs;
using ApplicationCore.CadNotasCore.MaterialCLVMs.Queries;
using Ardalis.Specification;
using Domain.Entities.EntitiesCad;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Specifications
{
    public class GetCLVMbyIdSpecification : Specification<EntityClvm, CLVMbyIdVM>
    {
        public GetCLVMbyIdSpecification(int id) 
        {
            Query.AsNoTracking().Where(x => x.Id == id)
                .Select(i => new CLVMbyIdVM
                {
                    Id = i.Id,
                    Item = i.Item,
                    Codigo = i.Codigo,
                    TipoComponente = i.TipoComponente,
                    Certificacao = i.Certificacao,
                    Corrida = i.Corrida,
                    Qtd = i.Qtd,
                    UnidadeMedida = i.UnidadeMedida,
                    LVMOrigem = i.LVMOrigem,
                    Programacao = i.Programacao,
                    Status = i.Status,
                    PO = i.PO,
                    Observacao = i.Observacao,
                    IdInspetor = i.IdInspetor,
                    DtInspecao = i.DtInspecao,
                    IdMaterialCLVM = i.IdCodigoMaterial,
                    Material = new MaterialCLVMVM
                    {
                        Especificacao = i.MaterialCLVM.Especificacao,
                        Diametro1 = i.MaterialCLVM.Diametro1,
                        Diametro2 = i.MaterialCLVM.Diametro2,
                        Comprimento = i.MaterialCLVM.Comprimento,
                        Espessura = i.MaterialCLVM.Espessura,
                        Largura = i.MaterialCLVM.Largura,
                        Peso = i.MaterialCLVM.Peso
                    }
                });
        }
    }
}
