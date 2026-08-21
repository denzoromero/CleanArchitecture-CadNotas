using ApplicationCore.CadNotasCore.CLVMs.VMs;
using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.RDFAs.Queries.GetRDFAPage
{
    public record GetRDFAPageVM
    {
        public NotaFiscaisCLVM Nota { get; init; } = null!;
        public IEnumerable<CLVMVM>? CLVMs { get; init; } = [];
    }

    //public record RDFAVM : CommonVM
    //{
    //    public string? NoRDFA { get; init; } = string.Empty;
    //    public int? NoPendencia { get; init; }

    //}
}
