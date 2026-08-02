using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad.ECadFornecedor
{
    public record FornecedorPO(
        string Fornecedor,
        string Fantasia,
        string? Contato,
        string IE,
        string CNPJ,
        string? Rua,
        string? Numero,
        string? Bairro,
        int? IdCidade,
        int? IdEstado,
        string? Cep,
        string? Telefone1,
        string? Telefone2,
        string? EMail,
        string? HomePage,
        int IdUser
    );
}
