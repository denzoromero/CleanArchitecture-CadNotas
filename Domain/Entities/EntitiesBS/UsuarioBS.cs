using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.EntitiesBS
{
    [Table("Usuario")]
    public class UsuarioBS : BaseEntity, IAggregateRoot
    {
        public int IdTerceiro { get; private set; }
        public int CodColigada { get; private set; }
        public string Chapa { get; private set; } = string.Empty;
        public string Senha { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        private UsuarioBS()
        {
        }

        public UsuarioBS(string chapa, string senha, string email)
        {
            Chapa = chapa;
            Senha = senha;
            Email = email;

            Ativo = 1;
        }

        public UsuarioBS(string chapa, string senha, string email, int ativo)
        {
            Chapa = chapa;
            Senha = senha;
            Email = email;
            Ativo = ativo;
        }




    }
}
