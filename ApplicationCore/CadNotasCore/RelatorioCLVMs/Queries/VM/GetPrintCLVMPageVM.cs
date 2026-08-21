using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.RelatorioCLVMs.Queries.VM
{
    public record GetPrintCLVMPageVM
    {
        public int? Id { get; init; }
        public int IdLVM { get; init; }
        public string IdLVMs { get; init; } = string.Empty;
        public string? Observacao { get; set; }
        public int? IdInspetor { get; set; }
        public string? InspetorName { get; set; }
        public DateTime? DtInspetor { get; set; }
        public string? DtInspetorString => DtInspetor?.ToString("dd/MM/yyyy");
        public int? IdVerificador { get; set; }
        public string? VerificadorName { get; set; }
        public DateTime? DtVerificar { get; set; }
        public string? DtVerificadorString => DtVerificar?.ToString("dd/MM/yyyy");

    }
}
