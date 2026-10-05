using Ardalis.GuardClauses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad.ECadFornecedor
{
    public class EntityFornecedor : BaseEntity, IAggregateRoot
    {
        public int IdAntigo { get; private set; }
        public string Fornecedor { get; private set; } = string.Empty;
        public string Fantasia { get; private set; } = string.Empty;
        public string IE { get; private set; } = string.Empty;
        public string CNPJ { get; private set; } = string.Empty;
        public string? Contato { get; private set; }
        public string? Rua { get; private set; }
        public string? Numero { get; private set; }
        public string? Bairro { get; private set; }
        public int? IdCidade { get; private set; }
        public string? CidadeBaseAntiga { get; private set; }
        public int? IdEstado { get; private set; }
        public char? EstadoBaseAntiga { get; private set; }
        public string? Cep { get; private set; }
        public string? Telefone1 { get; private set; }
        public string? Telefone2 { get; private set; }
        public string? Fax { get; private set; } 
        public string? EMail { get; private set; }
        public string? HomePage { get; private set; }
        public int? Usuario { get; private set; }

        private EntityFornecedor() { }

        public EntityFornecedor(string fornecedor, string fantasia, string ie, string cnpj)
        {
            Fornecedor = fornecedor;
            Fantasia = fantasia;
            IE = ie;
            CNPJ = cnpj;

            Ativo = 1;
        }

        public static EntityFornecedor Create(FornecedorPO param)
        {

            Guard.Against.NullOrEmpty(param.Fornecedor, nameof(param.Fornecedor));
            Guard.Against.NullOrEmpty(param.Fantasia, nameof(param.Fantasia));
            Guard.Against.NullOrEmpty(param.IE, nameof(param.IE));
            Guard.Against.NullOrEmpty(param.CNPJ, nameof(param.CNPJ));
            Guard.Against.NegativeOrZero(param.IdUser, nameof(param.IdUser));

            var fornecedor = new EntityFornecedor(param.Fornecedor, param.Fantasia, param.IE, param.CNPJ)
            {
                Contato = param.Contato,
                Rua = param.Rua,
                Numero = param.Numero,
                Bairro = param.Bairro,
                IdCidade = param.IdCidade,
                IdEstado = param.IdEstado,
                Cep = param.Cep,
                Telefone1 = param.Telefone1,
                Telefone2 = param.Telefone2,
                EMail = param.EMail,
                HomePage = param.HomePage,
                DataRegistro = DateTime.UtcNow,
                Usuario = param.IdUser
            };

            return fornecedor;
        }

        public void Update(FornecedorPO param)
        {
            Fornecedor = param.Fornecedor;
            Fantasia = param.Fantasia;
            IE = param.IE;
            CNPJ = param.CNPJ;
            Contato = param.Contato;
            Rua = param.Rua;
            Numero = param.Numero;
            Bairro = param.Bairro;
            IdCidade = param.IdCidade;
            IdEstado = param.IdEstado;
            Cep = param.Cep;
            Telefone1 = param.Telefone1;
            Telefone2 = param.Telefone2;
            EMail = param.EMail;
            HomePage = param.HomePage;
        }

        //public void CreateFornecedor(objectname param)
        //{
        //    ....
        //}



    }
}
