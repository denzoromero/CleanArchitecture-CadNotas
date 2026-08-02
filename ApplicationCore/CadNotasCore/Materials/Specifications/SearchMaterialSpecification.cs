using ApplicationCore.CadNotasCore.Materials.Queries;
using Ardalis.Specification;
using Domain;
using Domain.Entities.EntitiesCad.ECadMaterial;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.CadNotasCore.Materials.Specifications
{
    public class SearchMaterialSpecification : Specification<CadMaterial, MaterialVM>
    {
        public static Expression<Func<CadMaterial, bool>> BuildFilter(string? filter, int ativo)
        {
            return i => (string.IsNullOrEmpty(filter)
                                                    || i.Material.Contains(filter))
                                                    && i.Ativo == ativo;
        }

        public SearchMaterialSpecification(string? filter, int ativo, int pageNo = 0)
        {
            Query.AsNoTracking().Where(BuildFilter(filter, ativo))
                           .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                           .Take(Constants.ITEMS_PER_PAGE)
                           .Select(i => new MaterialVM
                           {
                               Id = i.Id,
                               Material = i.Material,
                           });
        }

    }
}
