using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesBS
{
    public class Cidade : BaseEntity, IAggregateRoot
    {
        public string? IdEstado { get; set; }
        public string? Nome { get; set; }

        private Cidade() { }

        public Cidade(string idEstado, string nome)
        {
            IdEstado = idEstado;
            Nome = nome;
            Ativo = 1;
        }

    }
}
