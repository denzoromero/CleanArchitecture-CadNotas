using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Obras
{
    public record ObraVM : CommonVM
    {
        public int? OldId { get; init; }
        public string Obra { get; init; } = string.Empty;
        public string Cliente { get; init; } = string.Empty;
        public string? Descricao { get; init; }
        public string? Contrato { get; init; }
        public string? Mascara { get; init; }
        public string? Ultlvm { get; init; }
        public string? Transferencia { get; init; }
    }
}
