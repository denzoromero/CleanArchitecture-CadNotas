using ApplicationCore.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.VMs
{
    public record CLVMVM : CommonVM
    {
        public decimal? Item { get; init; }
        public string? Codigo { get; init; }
        public string? TipoComponente { get; init; }
        public string? Certificacao { get; init; }
        public string? Corrida { get; init; }
        public decimal? Qtd { get; init; }
        public UMedida? UnidadeMedida { get; init; }
        public string? LVMOrigem { get; init; }
        public int? Programacao { get; init; }
        public ClvmStatus? Status { get; init; }
        public string? Check { get; init; }
        public int? TransferStatus { get; init; }
    }
}
