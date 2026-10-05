using Domain.Entities.EntitiesCad;
using Domain.Entities.EntitiesCad.ECadProjeto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataCad.Configurations
{
    public class EntityProcedimentoConfig : IEntityTypeConfiguration<CadProcedimento>
    {
        public void Configure(EntityTypeBuilder<CadProcedimento> builder)
        {
            builder
            .HasOne(p => p.Obra)
            .WithMany(o => o.Procedimentos)
            .HasForeignKey(p => p.IdObra)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
