using Domain.Entities.EntitiesCad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataCad.Configurations
{
    public class CadClvmConfig : IEntityTypeConfiguration<CadClvm>
    {
        public void Configure(EntityTypeBuilder<CadClvm> builder)
        {
            builder
            .HasOne(p => p.LVM)
            .WithMany(x => x.CLVMs)
            .HasForeignKey(p => p.IdLVM)
            .OnDelete(DeleteBehavior.Restrict);

            builder
              .HasOne(p => p.MaterialCLVM)
              .WithMany()
              .HasForeignKey(p => p.IdCodigoMaterial)
              .OnDelete(DeleteBehavior.Restrict);




        }
    }
}
