using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands
{
    public abstract record CLVMDTO
    {
        public int IdLVM { get; init; }
        public int? CodObra { get; init; }
        public decimal Item { get; init; }
        public string Codigo { get; init; } = string.Empty;
        public int PO { get; init; }
        public string? TipoComponente { get; init; }
        public string? Certificacao { get; init; }
        public string? Corrida { get; init; }
        public string? TMA { get; init; }
        public string? LVMOrigem { get; init; }
        public UMedida? UnidadeMedida { get; init; }
        public decimal? Qtd { get; init; }
        public DateTime DtInspecao { get; init; }
        public int? Programacao { get; init; }
        public ClvmStatus Status { get; init; }
        public string? Observacao { get; init; }
        public int IdMaterialCLVM { get; init; }
    }
}
