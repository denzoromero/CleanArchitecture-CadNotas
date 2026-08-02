using Ardalis.Specification;
using Domain.Entities.EntitiesCad.ECadProjeto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.Projetos.Specifications
{
    public sealed class ProjetoById : Specification<CadProjeto>
    {
        public ProjetoById(int id)
        {
            Query.Where(x => x.Id == id)
                .Include(x => x.Obras);
        }
    }
}
