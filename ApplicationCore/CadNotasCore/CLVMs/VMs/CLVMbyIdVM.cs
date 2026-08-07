using ApplicationCore.CadNotasCore.MaterialCLVMs.Queries;
using ApplicationCore.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.VMs
{
    public record CLVMbyIdVM : CommonVM
    {
        public decimal Item { get; init; }
        public string Codigo { get; init; } = string.Empty;
        public string? TipoComponente { get; init; }
        public string? Certificacao { get; init; }
        public string? Corrida { get; init; }
        public DateTime DtInspecao { get; init; }
        public decimal? Qtd { get; init; }
        public UMedida? UnidadeMedida { get; init; }
        public string? LVMOrigem { get; init; }
        public int? Programacao { get; init; }
        public ClvmStatus Status { get; init; }
        public int PO { get; init; }
        public string? Observacao { get; init; }
        public int? IdInspetor { get; init; }
        public int? IdMaterialCLVM { get; init; }
        public MaterialCLVMVM Material { get; init; } = null!;
    }
}
