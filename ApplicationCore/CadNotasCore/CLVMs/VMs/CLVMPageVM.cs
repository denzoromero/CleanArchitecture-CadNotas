using ApplicationCore.CadNotasCore.NotaFiscaisCore.Queries.VMs;
using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.VMs
{
    public record CLVMPageVM
    {
        public NotaFiscaisCLVM Nota { get; init; } = null!;
        public IEnumerable<DropdownListVM> Procedimentos { get; init; } = [];
        public IEnumerable<CLVMVM>? CLVMs { get; init; } = [];
    }
}
