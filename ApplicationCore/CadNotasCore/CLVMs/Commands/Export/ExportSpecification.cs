using Ardalis.Specification;
using Domain.Entities.EntitiesCad;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Export
{
    public class ExportSpecification : Specification<EntityClvm, ExportVM>
    {
        public ExportSpecification(int mascara)
        {
            Query.AsNoTracking().Where(i => i.LVM.Id == mascara
                        && i.Ativo == 1 && (i.Status == ClvmStatus.Aprovado || i.Status == ClvmStatus.ParcialmenteAprovado))
                .Select(x => new ExportVM
                {
                    Fabricante = "ANGRA YARD",
                    NumeroRIR = x.LVM.MascaraLVM,
                    Item = x.Item,
                    DtInspecao = x.DtInspecao,
                    Fornecedor = x.LVM.Fornecedor != null ? x.LVM.Fornecedor.Fornecedor : string.Empty,
                    NotaFiscal = x.LVM.NotaFiscal,
                    CodigoMaterial = x.Codigo.StartsWith("BB-") ? x.Codigo.Substring(3) : x.Codigo,
                    Diametro1 = x.MaterialCLVM.Diametro1,
                    Diametro2 = x.MaterialCLVM.Diametro2,
                    Certificado = x.Certificacao,
                    Corrida = x.Corrida,
                    CodigoInternoMaterial = $"{x.LVM.MascaraLVM}-{x.Item} {x.Corrida}",
                    Quantidade = x.Qtd,
                    Inspetor = "0",
                    NumeroCertificadoInspetor = x.IdInspetor.ToString(),
                    StatusName = x.Status.ToString(),
                    Obs = x.Observacao,
                    CodigoStru = $"{x.LVM.NLVM}-{x.Corrida}",
                    EspecificacaoMaterial = x.MaterialCLVM.Especificacao,
                    TipoComponente = x.MaterialCLVM.TipoComponenteMaterial,
                    Comprimento = x.MaterialCLVM.Comprimento,
                    Espessura = x.MaterialCLVM.Espessura,
                    Largura = x.MaterialCLVM.Largura,
                    Peso = x.MaterialCLVM.Peso,
                    UMName = x.UnidadeMedida.ToString(),
                    Contrato = x.LVM.Obra.Contrato
                });
        }
    }
}
