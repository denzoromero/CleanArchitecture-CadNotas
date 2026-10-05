using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesCad;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using Domain.Entities.EntitiesCad.ECadMaterial;
using Domain.Entities.EntitiesCad.ECadObra;
using Domain.Entities.EntitiesCad.ECadProjeto;
using Domain.Entities.EntitiesCad.ECadTransportadora;
using Domain.Entities.EntitiesCad.EObraVSProjeto;
using Infrastructure.DataBS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.DataCad
{
    public class ContextCad : DbContext, IContextCad
    {
        #pragma warning disable CS8618 // Required by Entity Framework
        public ContextCad(DbContextOptions<ContextCad> options) : base(options) { }

        public DbSet<EntityFornecedor> CadFornecedors => Set<EntityFornecedor>();
        public DbSet<EntityTransportadora> CadTransportadoras => Set<EntityTransportadora>();
        public DbSet<EntityObra> CadObras => Set<EntityObra>();
        public DbSet<EntityMaterial> CadMaterials => Set<EntityMaterial>();
        public DbSet<EntityProjeto> CadProjetos => Set<EntityProjeto>();
        public DbSet<ObraVSProjeto> ObraVSProjetos => Set<ObraVSProjeto>();
        public DbSet<EntityDisciplina> CadDisciplinas => Set<EntityDisciplina>();
        public DbSet<CadTipoOC> CadTipoOCs => Set<CadTipoOC>();
        public DbSet<EntityMaterialCLVM> CadMaterialCLVMs => Set<EntityMaterialCLVM>();
        public DbSet<CadProcedimento> CadProcedimentos => Set<CadProcedimento>();
        public DbSet<EntityLVM> CadLVMs => Set<EntityLVM>();
        public DbSet<EntityClvm> CadClvms => Set<EntityClvm>();
        public DbSet<ReportCLVM> RelatorioCLVMs => Set<ReportCLVM>();
        public DbSet<IdempotencyRequest> IdempotencyRequests => Set<IdempotencyRequest>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
