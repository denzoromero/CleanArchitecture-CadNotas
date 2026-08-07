using ApplicationCore.CadNotasCore.CLVMs.VMs;
using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries.VMs
{
    public record NotaFiscaisVM : CommonVM
    {
        public int IdObra { get; init; }
        public string Obra { get; init; } = string.Empty;
        public string? MascaraLVM { get; init; }
        public string? NLVM { get; init; }
        public string? Documento { get; init; }
        
        public string? NumeroLVM { get; init; }
        public string? LVM { get; init; }
        public DateTime Data { get; init; }
        public string DataString => Data.ToString("dd/MM/yyyy");
        public string? NotaFiscal { get; init; }
        public string? OC { get; init; }
        public string? Invoice { get; init; }
        public string? QtdItem { get; init; }
        public string? Material { get; init; }
        public string? Disciplina { get; init; }
        public string? TipoOC { get; init; }


        public string? RM { get; init; }
        public string? RC { get; init; }
        public decimal? Valor { get; init; }
        public string? Obs { get; init; }

        public int? IdFornecedor { get; init; }
        public int? IdMaterial { get; init; }
        public int? IdDisciplina { get; init; }
        public int? IdTipoOC { get; init; }
        public int? IdProcedimento { get; init; }

      


    }
}
