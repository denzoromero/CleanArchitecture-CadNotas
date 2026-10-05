using Ardalis.GuardClauses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad
{
    public class EntityDisciplina : BaseEntity, IAggregateRoot
    {
        public string Disciplina { get; private set; } = string.Empty;

        private EntityDisciplina() { }

        public EntityDisciplina(string disciplina)
        {
            Guard.Against.NullOrEmpty(disciplina, nameof(disciplina));
            Disciplina = disciplina;
            Ativo = 1;
        }

        public static EntityDisciplina Create(string disciplina)
        {
            Guard.Against.NullOrEmpty(disciplina, nameof(disciplina));

            var entity = new EntityDisciplina(disciplina);

            return entity;
        }

        public void Update(string disciplina)
        {
            Guard.Against.NullOrEmpty(disciplina, nameof(disciplina));
            Disciplina = disciplina;
        }

    }
}
