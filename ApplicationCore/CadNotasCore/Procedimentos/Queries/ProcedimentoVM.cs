using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Procedimentos.Queries
{
    public record ProcedimentoVM : CommonVM
    {
        public int? IdObra { get; init; }
        public string? Obra { get; init; }
        public string Procedimento { get; init; } = string.Empty;
        public string? Revisao { get; init; }
    }
}
