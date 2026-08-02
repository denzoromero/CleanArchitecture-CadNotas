using Ardalis.Specification;
using Domain;
using Domain.Entities.EntitiesCad.ECadProjeto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.CadNotasCore.Projetos.Specifications
{
    public class SearchProjetosSpecification : Specification<CadProjeto, ProjetosVM>
    {
        public static Expression<Func<CadProjeto, bool>> BuildFilter(string? filter, int ativo)
        {
            return i => (string.IsNullOrEmpty(filter)
                                                             || i.Projeto.Contains(filter))
                                                             && i.Ativo == ativo;
        }

        public SearchProjetosSpecification(string? filter, int ativo, int pageNo = 0)
        {
            Query.AsNoTracking().Where(BuildFilter(filter, ativo))
                      .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                                .Take(Constants.ITEMS_PER_PAGE)
                .Select(x => new ProjetosVM
                {
                    Id = x.Id,
                    Projeto = x.Projeto,
                    Obras = string.Join("; ",x.Obras.Select(y => y.Obra.Obra)),
                    IdObras = x.Obras.Select(z => z.IdObra)
                });
        }

    }
}
