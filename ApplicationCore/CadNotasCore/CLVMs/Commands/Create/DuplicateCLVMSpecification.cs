using Ardalis.Specification;
using Domain.Entities.EntitiesCad;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Create
{
    public class DuplicateCLVMSpecification : Specification<EntityClvm>
    {
        public DuplicateCLVMSpecification(int idLVM, decimal item, int? excludeId = null) 
        {
            Query.AsNoTracking().Where(x => x.IdLVM == idLVM && x.Item == item);

            if (excludeId.HasValue)
            {
                Query.Where(x => x.Id != excludeId.Value);
            }
        }
    }
}
