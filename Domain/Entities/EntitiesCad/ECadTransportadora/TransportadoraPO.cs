using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad.ECadTransportadora
{
    public record TransportadoraPO(
        string Nome,
        string IE,
        string CNPJ,
        string? ConhecTransp,
        string? Rua,
        char? Numero,
        string? Bairro,
        int? IdCidade,
        int? IdEstado,
        string? Cep,
        string? Telefone1,
        string? Telefone2,
        string? Fax,
        string? EMail,
        string? HomePage
    );
}
