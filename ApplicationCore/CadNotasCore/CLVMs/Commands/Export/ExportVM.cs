using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Export
{
    public record ExportVM
    {
        public string? Fabricante { get; init; }
        public string? NumeroRIR { get; init; }
        public decimal? Item { get; init; }
        public DateTime DtInspecao { get; init; }
        public string? DateString => DtInspecao.ToString("dd/mm/yyyy");
        public string? Fornecedor { get; init; }
        public string? NotaFiscal { get; init; }
        public string? CodigoMaterial { get; init; }
        public string? Diametro1 { get; init; }
        public string? Diametro2 { get; init; }
        public string? Certificado { get; init; }
        public string? Corrida { get; init; }
        public string? CodigoInternoMaterial { get; init; }
        public decimal? Quantidade { get; init; }
        public string? Inspetor { get; init; }
        public string? NumeroCertificadoInspetor { get; init; }
        public string? StatusName { get; init; }
        public string? Obs { get; init; }
        public string? CodigoStru { get; init; }
        public string? EspecificacaoMaterial { get; init; }
        public string? TipoComponente { get; init; }
        public string? Comprimento { get; init; }
        public string? Espessura { get; init; }
        public string? Largura { get; init; }
        public string? Peso { get; init; }
        public string? UMName { get; init; }
        public string? RIALNumeroRIR { get; init; }
        public string? RIALCodigoInterno { get; init; }
        public string? Contrato { get; init; }
    }
}
