using Domain.Entities.EntitiesCad;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using Domain.Entities.EntitiesCad.ECadMaterial;
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
        DbSet<CadFornecedor> CadFornecedors { get; }
        DbSet<CadMaterial> CadMaterials { get; }
        DbSet<CadDisciplina> CadDisciplinas { get; }
        DbSet<CadTipoOC> CadTipoOCs { get; }
        DbSet<CadProjeto> CadProjetos { get; }
        DbSet<CadLVM> CadLVMs { get; }
        DbSet<ObraVSProjeto> ObraVSProjetos { get; }
    }
}
