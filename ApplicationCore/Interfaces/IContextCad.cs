using Domain.Entities.EntitiesCad.ECadObra;
using Domain.Entities.EntitiesCad.ECadProjeto;
using Domain.Entities.EntitiesCad.EObraVSProjeto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IContextCad
    {
        DbSet<CadObra> CadObras { get; }
        DbSet<CadProjeto> CadProjetos { get; }
        DbSet<ObraVSProjeto> ObraVSProjetos { get; }
    }
}
