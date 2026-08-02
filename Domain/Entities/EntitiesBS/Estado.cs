using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesBS
{
    public class Estado : BaseEntity, IAggregateRoot
    {
        public string Uf { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;

        private Estado() { }

        public Estado(string uf, string nome)
        {
            Uf = uf;
            Nome = nome;
            Ativo = 1;
        }


    }
}
