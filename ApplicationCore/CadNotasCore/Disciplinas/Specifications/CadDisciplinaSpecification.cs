using Ardalis.Specification;
using Domain;
using Domain.Entities.EntitiesCad;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.CadNotasCore.Disciplinas.Specifications
{
    public sealed class CadDisciplinaSpecification : Specification<EntityDisciplina,DisciplinaVM>
    {
        public static Expression<Func<EntityDisciplina, bool>> BuildFilter(string? filter, int ativo)
        {
            return i => (string.IsNullOrEmpty(filter)
                                                || i.Disciplina.Contains(filter))
                                                && i.Ativo == ativo;
        } 

        public CadDisciplinaSpecification(string? filter, int ativo, int pageNo = 0)
        {
            Query.AsNoTracking().Where(BuildFilter(filter, ativo))
                             .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                                .Take(Constants.ITEMS_PER_PAGE)
                                .Select(i => new DisciplinaVM
                                {
                                    Id = i.Id,
                                    Disciplina = i.Disciplina
                                });
        }

    }
}
