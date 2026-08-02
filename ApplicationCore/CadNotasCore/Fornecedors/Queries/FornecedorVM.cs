using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Fornecedors.Queries
{
    public record FornecedorVM : CommonVM
    {
        public int IdAntigo { get; init; }
        public string Fornecedor { get; init; } = string.Empty;
        public string Fantasia { get; init; } = string.Empty;
        public string? Contato { get; init; }
        public string IE { get; init; } = string.Empty;
        public string CNPJ { get; init; } = string.Empty;
        public string? Rua { get; init; }
        public string? Numero { get; init; }
        public string? Bairro { get; init; }
        public int? IdCidade { get; init; }
        public string? CidadeBaseAntiga { get; init; }
        public int? IdEstado { get; init; }
        public char? EstadoBaseAntiga { get; init; }
        public string? Cep { get; init; }
        public string? Telefone1 { get; init; }
        public string? Telefone2 { get; init; }
        public string? Fax { get; init; }
        public string? EMail { get; init; }
        public string? HomePage { get; init; }
        public int? Usuario { get; init; }

    }
}
