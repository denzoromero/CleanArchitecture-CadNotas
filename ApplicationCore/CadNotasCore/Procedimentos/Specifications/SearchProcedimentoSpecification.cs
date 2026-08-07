using ApplicationCore.CadNotasCore.Procedimentos.Queries;
using Ardalis.Specification;
using Domain;
using Domain.Entities.EntitiesCad;
using System.Linq.Expressions;

namespace ApplicationCore.CadNotasCore.Procedimentos.Specifications
{
    public class SearchProcedimentoSpecification : Specification<CadProcedimento, ProcedimentoVM>
    {
        public static Expression<Func<CadProcedimento, bool>> BuildFilter(string? filter, int ativo)
        {
            return i => (string.IsNullOrEmpty(filter) || i.Procedimento.Contains(filter))
                                                                     && i.Ativo == ativo;
        }

        public SearchProcedimentoSpecification(string? filter, int ativo, int pageNo = 0)
        {
            Query.AsNoTracking().Include(x => x.Obra)
                        .Where(BuildFilter(filter, ativo))
                         .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                         .Take(Constants.ITEMS_PER_PAGE)
                         .Select(i => new ProcedimentoVM
                         {
                             Id = i.Id,
                             IdObra = i.Obra != null ? i.Obra.Id : null,
                             Obra = i.Obra != null ? i.Obra.Obra : null,
                             Procedimento = i.Procedimento,
                             Revisao = i.Revisao
                         });
        }

    }
}
