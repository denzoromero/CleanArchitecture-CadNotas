using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.MaterialCLVMs.Queries
{
    public record MaterialCLVMVM : CommonVM
    {
        public string Codigo { get; init; } = string.Empty;
        public string? Especificacao { get; init; }
        public string? Descricao { get; init; }
        public string? Diametro1 { get; init; }
        public string? Diametro2 { get; init; }
        public string? Comprimento { get; init; }
        public string? Espessura { get; init; }
        public string? Largura { get; init; }
        public string? Peso { get; init; }
        public string? TipoComponenteMaterial { get; init; }
    }
}
