using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Transportadoras.Commands
{
    public abstract record TransportadoraDTO
    {
        public string Nome { get; init; } = string.Empty;
        public string IE { get; init; } = string.Empty;
        public string CNPJ { get; init; } = string.Empty;
        public string? ConhecTransp { get; init; }
        public string? Rua { get; init; }
        public char? Numero { get; init; }
        public string? Bairro { get; init; }
        public int? IdCidade { get; init; }
        public int? IdEstado { get; init; }
        public string? Cep { get; init; }
        public string? Telefone1 { get; init; }
        public string? Telefone2 { get; init; }
        public string? Fax { get; init; }
        public string? EMail { get; init; }
        public string? HomePage { get; init; }
    }

}
