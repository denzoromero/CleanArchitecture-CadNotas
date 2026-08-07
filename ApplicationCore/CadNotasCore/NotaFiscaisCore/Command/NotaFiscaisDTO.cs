using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Command
{
    public abstract record NotaFiscaisDTO
    {
        public int IdObra { get; init; }
        public string? NLVM { get; init; }
        public string? MascaraLVM { get; init; }
        public string? NotaFiscal { get; init; }
        public string? Documento { get; init; }
        public DateTime Data { get; init; }
        public int? IdFornecedor { get; init; }
        public string? OC { get; init; }
        public string? RM { get; init; }
        public string? RC { get; init; }
        public int? IdMaterial { get; init; }
        public int? IdDisciplina { get; init; }
        public int? IdTipoOC { get; init; }
        public decimal? Valor { get; init; }
        public string? Obs { get; init; }
    }
}
