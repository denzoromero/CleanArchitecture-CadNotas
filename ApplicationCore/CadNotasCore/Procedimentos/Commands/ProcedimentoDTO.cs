using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Procedimentos.Commands
{
    public abstract record ProcedimentoDTO()
    {
        public string Procedimento { get; init; } = string.Empty;
        public int? IdObra { get; init; }
        public string? Revisao { get; init; }
    };
}
