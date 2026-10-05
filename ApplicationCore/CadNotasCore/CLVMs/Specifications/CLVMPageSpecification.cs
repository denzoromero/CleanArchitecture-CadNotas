using ApplicationCore.CadNotasCore.CLVMs.VMs;
using Ardalis.Specification;
using Domain.Entities.EntitiesCad;
using Domain.Entities.EntitiesCad.ECadObra;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ApplicationCore.CadNotasCore.CLVMs.Specifications
{
    public class CLVMPageSpecification : Specification<EntityClvm, CLVMVM>
    {
        public static Expression<Func<EntityClvm, bool>> BuildFilter(int idLVM)
        {
            return i => i.LVM.Id == idLVM && i.Ativo == 1;
        }

        public CLVMPageSpecification(int idLVM)
        {
            Query.AsNoTracking().Where(BuildFilter(idLVM))
                .Select(i => new CLVMVM
                {
                    Item = i.Item,
                    Codigo = i.Codigo,
                    TipoComponente = i.TipoComponente,
                    Certificacao = i.Certificacao,
                    Corrida = i.Corrida,
                    Qtd = i.Qtd,
                    UnidadeMedida = i.UnidadeMedida,
                    LVMOrigem = i.LVMOrigem,
                    Programacao = i.Programacao,
                    Status = i.Status,
                });
        }

    }
}
