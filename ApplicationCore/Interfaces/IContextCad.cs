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
        DbSet<EntityObra> CadObras { get; }
        DbSet<EntityFornecedor> CadFornecedors { get; }
        DbSet<EntityMaterial> CadMaterials { get; }
        DbSet<EntityDisciplina> CadDisciplinas { get; }
        DbSet<CadTipoOC> CadTipoOCs { get; }
        DbSet<EntityProjeto> CadProjetos { get; }
        DbSet<EntityLVM> CadLVMs { get; }
        DbSet<ObraVSProjeto> ObraVSProjetos { get; }
    }
}
