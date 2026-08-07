using ApplicationCore.Common;
using Ardalis.Specification;
using Domain.Entities.EntitiesCad;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Specifications
{
    public class GetProcedimentosSpecificaiton : Specification<CadProcedimento, DropdownListVM>
    {
        public GetProcedimentosSpecificaiton()
        {
            Query.AsNoTracking().Where(i => i.Ativo == 1)
                .Select(i => new DropdownListVM
                {
                    Id = i.Id,
                    Name = $"{i.Procedimento}-{i.Revisao}"
                });
        }
    }
}
