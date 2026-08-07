using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.VMs
{
    public record NotaFiscaisCLVM : CommonVM
    {
        public int IdObra { get; init; }
        public string Obra { get; init; } = string.Empty;
        public string? MascaraLVM { get; init; }
        public string? NLVM { get; init; }
        public DateTime Data { get; init; }
        public string DataString => Data.ToString("dd/MM/yyyy");
        public string? NotaFiscal { get; init; }
        public string? Fornecedor { get; init; }
        public string? Transportadora { get; init; }
        public string? OC { get; init; }
        public string? Invoice { get; init; }
        public string? QtdItem { get; init; }
        public string? Material { get; init; }
        public string? Disciplina { get; init; }
        public string? TipoOC { get; init; }
        public string? Obs { get; init; }
        public int? IdTipoOC { get; init; }
        public int? IdProcedimento { get; init; }
        public IEnumerable<CLVMVM>? Materials { get; init; } = [];
    }
}
