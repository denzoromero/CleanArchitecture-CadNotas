using Ardalis.Specification;
using Domain;
using Domain.Entities.EntitiesCad;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.CadNotasCore.TipoOCs.Specifications
{
    public sealed class CadTipoOCSpecifcation : Specification<CadTipoOC, TipoOCVM>
    {
        public static Expression<Func<CadTipoOC, bool>> BuildFilter(string? filter, int ativo)
        {
            return i => (string.IsNullOrEmpty(filter)
                                                    || i.Nome.Contains(filter))
                                                    && i.Ativo == ativo;
        }

        public CadTipoOCSpecifcation(string? filter, int ativo, int pageNo = 0)
        {
            Query.AsNoTracking().Where(BuildFilter(filter, ativo))
                               .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                                .Take(Constants.ITEMS_PER_PAGE)
                                .Select(i => new TipoOCVM(i.Id,i.Nome));
        }
    }
}
