using Ardalis.GuardClauses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad.ECadTransportadora
{
    public class CadTransportadora : BaseEntity, IAggregateRoot
    {
        public string Nome { get; private set; } = string.Empty;
        public string IE { get; private set; } = string.Empty;
        public string CNPJ { get; private set; } = string.Empty;
        public string? ConhecTransp { get; private set; }
        public string? Rua { get; private set; }
        public char? Numero { get; private set; }
        public string? Bairro { get; private set; }
        public int? IdCidade { get; private set; }
        public int? IdEstado { get; private set; }
        public string? Cep { get; private set; }
        public string? Telefone1 { get; private set; }
        public string? Telefone2 { get; private set; }
        public string? Fax { get; private set; }
        public string? EMail { get; private set; }
        public string? HomePage { get; private set; }

        private CadTransportadora() { }

        public CadTransportadora(string nome, string ie, string cnpj)
        {
            Nome = nome;
            IE = ie;
            CNPJ = cnpj;
            Ativo = 1;
        }

        public static CadTransportadora Create(TransportadoraPO param)
        {
            Guard.Against.NullOrEmpty(param.Nome, nameof(param.Nome));
            Guard.Against.NullOrEmpty(param.IE, nameof(param.IE));
            Guard.Against.NullOrEmpty(param.CNPJ, nameof(param.CNPJ));

            var transportadora = new CadTransportadora(param.Nome, param.IE, param.CNPJ)
            {
                ConhecTransp = param.ConhecTransp,
                Rua = param.Rua,
                Numero = param.Numero,
                Bairro = param.Bairro,
                IdCidade = param.IdCidade,
                IdEstado = param.IdEstado,
                Cep = param.Cep,
                Telefone1 = param.Telefone1,
                Telefone2 = param.Telefone2,
                Fax = param.Fax,
                EMail = param.EMail,
                HomePage = param.HomePage,
                DataRegistro = DateTime.UtcNow
            };

            return transportadora;
        }

        public void Update(TransportadoraPO param)
        {
            Guard.Against.NullOrEmpty(param.Nome, nameof(param.Nome));
            Guard.Against.NullOrEmpty(param.IE, nameof(param.IE));
            Guard.Against.NullOrEmpty(param.CNPJ, nameof(param.CNPJ));

            Nome = param.Nome;
            IE = param.IE;
            CNPJ = param.CNPJ;
            ConhecTransp = param.ConhecTransp;
            Rua = param.Rua;
            Numero = param.Numero;
            Bairro = param.Bairro;
            IdCidade = param.IdCidade;
            IdEstado = param.IdEstado;
            Cep = param.Cep;
            Telefone1 = param.Telefone1;
            Telefone2 = param.Telefone2;
            Fax = param.Fax;
            EMail = param.EMail;
            HomePage = param.HomePage;
        }


    }
}
