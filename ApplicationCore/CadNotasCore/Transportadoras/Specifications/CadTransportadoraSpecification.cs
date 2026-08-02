using Ardalis.Specification;
using Domain;
using Domain.Entities.EntitiesCad.ECadTransportadora;
using System.Linq.Expressions;

namespace ApplicationCore.CadNotasCore.Transportadoras.Specifications
{
    public class CadTransportadoraSpecification : Specification<CadTransportadora, TransportadoraVM>
    {
        public static Expression<Func<CadTransportadora, bool>> BuildFilter(string? filter, int ativo)
        {
            return i => (string.IsNullOrEmpty(filter)
                        || i.Nome.Contains(filter)
                        || i.IE.Contains(filter)
                        || i.CNPJ.Contains(filter))
                        && i.Ativo == ativo;
        }

        public CadTransportadoraSpecification(string? filter, int ativo, int pageNo = 0)
        {
            Query.AsNoTracking().Where(BuildFilter(filter, ativo))
                                .Skip((pageNo) * Constants.ITEMS_PER_PAGE)
                                .Take(Constants.ITEMS_PER_PAGE)
                                .Select(i => new TransportadoraVM
                                {
                                    Id = i.Id,
                                    Nome = i.Nome,
                                    IE = i.IE,
                                    CNPJ = i.CNPJ
                                });

        }
    }
}
