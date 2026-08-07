using ApplicationCore.CadNotasCore.CLVMs.VMs;
using ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries.VMs;
using Ardalis.Specification;
using Domain.Entities.EntitiesCad;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Specifications
{
    public class GetLVMByIdSpecification : Specification<CadLVM, NotaFiscaisCLVM>
    {
        public GetLVMByIdSpecification(int idLVM)
        {
            Query.AsNoTracking().Where(i => i.Id == idLVM)
                .Select(i => new NotaFiscaisCLVM
                {
                    Id = i.Id,
                    Obra = i.Obra.Obra,
                    MascaraLVM = i.MascaraLVM,
                    NLVM = i.NLVM,
                    NotaFiscal = i.NotaFiscal,
                    Data = i.Data,
                    Fornecedor = i.Fornecedor != null ? i.Fornecedor.Fornecedor : string.Empty,
                    OC = i.OC,
                    Invoice = i.RM,
                    QtdItem = i.RC,
                    Material = i.Material != null ? i.Material.Material : string.Empty,
                    Transportadora = i.Transportadora != null ? i.Transportadora.Nome : string.Empty,
                    Disciplina = i.Disciplina != null ? i.Disciplina.Disciplina : string.Empty,
                    TipoOC = i.TipoOC != null ? i.TipoOC.Nome : string.Empty,
                    Obs = i.Obs,
                    IdProcedimento = i.IdProcedimento,
                    Materials = i.CLVMs.Where(x => x.Ativo == 1).Select(x => new CLVMVM
                    {
                        Id = x.Id,
                        Item = x.Item,
                        Codigo = x.Codigo,
                        TipoComponente = x.TipoComponente,
                        Certificacao = x.Certificacao,
                        Corrida = x.Corrida,
                        Qtd = x.Qtd,
                        UnidadeMedida = x.UnidadeMedida,
                        LVMOrigem = x.LVMOrigem,
                        Programacao = x.Programacao,
                        Status = x.Status,
                        Ativo = x.Ativo,
                        TransferStatus = x.TransferStatus
                    })
                });
        }
    }
}
