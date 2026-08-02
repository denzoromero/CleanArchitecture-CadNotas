using ApplicationCore.Interfaces;
using Domain.Entities.EntitiesBS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.DataBS
{
    public class ContextBS : DbContext, IContextBS
    {
        #pragma warning disable CS8618 // Required by Entity Framework
        public ContextBS(DbContextOptions<ContextBS> options) : base(options) { }

        public DbSet<UsuarioBS> Usuarios => Set<UsuarioBS>();
        public DbSet<Estado> Estados => Set<Estado>();
        public DbSet<Cidade> Cidades => Set<Cidade>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
