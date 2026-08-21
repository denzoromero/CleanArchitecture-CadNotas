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

        public DbSet<CadFornecedor> CadFornecedors => Set<CadFornecedor>();
        public DbSet<CadTransportadora> CadTransportadoras => Set<CadTransportadora>();
        public DbSet<CadObra> CadObras => Set<CadObra>();
        public DbSet<CadMaterial> CadMaterials => Set<CadMaterial>();
        public DbSet<CadProjeto> CadProjetos => Set<CadProjeto>();
        public DbSet<ObraVSProjeto> ObraVSProjetos => Set<ObraVSProjeto>();
        public DbSet<CadDisciplina> CadDisciplinas => Set<CadDisciplina>();
        public DbSet<CadTipoOC> CadTipoOCs => Set<CadTipoOC>();
        public DbSet<CadMaterialCLVM> CadMaterialCLVMs => Set<CadMaterialCLVM>();
        public DbSet<CadProcedimento> CadProcedimentos => Set<CadProcedimento>();
        public DbSet<CadLVM> CadLVMs => Set<CadLVM>();
        public DbSet<CadClvm> CadClvms => Set<CadClvm>();
        public DbSet<RelatorioCLVM> RelatorioCLVMs => Set<RelatorioCLVM>();
        public DbSet<IdempotencyRequest> IdempotencyRequests => Set<IdempotencyRequest>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
