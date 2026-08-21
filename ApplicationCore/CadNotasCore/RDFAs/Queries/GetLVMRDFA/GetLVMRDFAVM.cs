using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.RDFAs.Queries.GetLVMRDFA
{
    public record GetLVMRDFAVM
    {
        public int IdLVM { get; init; }
        public string? NumeroLVM { get; init; }
        public string? NotaFiscal { get; init; }
    }
}
