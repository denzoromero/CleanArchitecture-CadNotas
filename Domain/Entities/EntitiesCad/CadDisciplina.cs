using Ardalis.GuardClauses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad
{
    public class CadDisciplina : BaseEntity, IAggregateRoot
    {
        public string Disciplina { get; private set; } = string.Empty;

        private CadDisciplina() { }

        public CadDisciplina(string disciplina)
        {
            Guard.Against.NullOrEmpty(disciplina, nameof(disciplina));
            Disciplina = disciplina;
            Ativo = 1;
        }

        public static CadDisciplina Create(string disciplina)
        {
            Guard.Against.NullOrEmpty(disciplina, nameof(disciplina));

            var entity = new CadDisciplina(disciplina);

            return entity;
        }

        public void Update(string disciplina)
        {
            Guard.Against.NullOrEmpty(disciplina, nameof(disciplina));
            Disciplina = disciplina;
        }

    }
}
