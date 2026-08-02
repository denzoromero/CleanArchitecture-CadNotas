using Ardalis.GuardClauses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad
{
    public class CadTipoOC : BaseEntity, IAggregateRoot
    {
        public string Nome { get; set; } = string.Empty;

        private CadTipoOC() { }

        public CadTipoOC(string nome)
        {
            Guard.Against.NullOrEmpty(nome, nameof(nome));
            Nome = nome;
            Ativo = 1;
        }

        public static CadTipoOC Create(string nome)
        {
            Guard.Against.NullOrEmpty(nome, nameof(nome));

            return new CadTipoOC(nome);
        }

        public void Update(string nome)
        {
            Guard.Against.NullOrEmpty(nome, nameof(nome));
            Nome = nome;
        }

    }
}
