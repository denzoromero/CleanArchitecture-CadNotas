using Ardalis.Specification;
using Domain;
using Domain.Entities.EntitiesCad.ECadObra;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.CadNotasCore.Obras.Specifications
{
    public class SearchObraSpecification : Specification<CadObra, ObraVM>
    {
        public static Expression<Func<CadObra, bool>> BuildFilter(string? filter, int ativo)
        {
            return i => (string.IsNullOrEmpty(filter)
                                                    || i.Obra.Contains(filter)
                                                    || i.Cliente.Contains(filter)
                                                    || (i.Contrato != null && i.Contrato.Contains(filter))
                                                    || (i.Descricao != null && i.Descricao.Contains(filter)))
                                                    && i.Ativo == ativo;
        }

        public SearchObraSpecification(string? filter, int ativo, int pageNo = 0)
        {
            Query.AsNoTracking().Where(BuildFilter(filter, ativo))
                               .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                               .Take(Constants.ITEMS_PER_PAGE)
                               .Select(i => new ObraVM
                               {
                                   Id = i.Id,
                                   Obra = i.Obra,
                                   Cliente = i.Cliente,
                                   Descricao = i.Descricao,
                                   Contrato = i.Contrato,
                                   Transferencia = i.Transferencia
                               });
        }
    }
}
