using ApplicationCore.CadNotasCore.Fornecedors.Queries;
using Ardalis.Specification;
using Domain;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ApplicationCore.CadNotasCore.Fornecedors.Specification
{
    public class CadFornecedorSpecificaiton : Specification<EntityFornecedor, FornecedorVM>
    {
        public static Expression<Func<EntityFornecedor, bool>> BuildFilter(string? filter,int ativo)
        {
            return i => (string.IsNullOrEmpty(filter)
                        || i.Fornecedor.Contains(filter)
                        || i.Fantasia.Contains(filter)
                        || i.IE.Contains(filter)
                        || i.CNPJ.Contains(filter))
                        && i.Ativo == ativo;
        }

        public CadFornecedorSpecificaiton(string? filter, int ativo, int pageNo = 0)
        {

            Query.AsNoTracking().Where(BuildFilter(filter, ativo))
                                .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                                .Take(Constants.ITEMS_PER_PAGE)
                                .Select(i => new FornecedorVM
                                {
                                    Id = i.Id,
                                    Fornecedor = i.Fornecedor,
                                    Fantasia = i.Fantasia,
                                    IE = i.IE,
                                    CNPJ = i.CNPJ
                                });
        }



    }
}
