using Domain.Entities.EntitiesCad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataCad.Configurations
{
    public class CadLVMConfig : IEntityTypeConfiguration<CadLVM>
    {
        public void Configure(EntityTypeBuilder<CadLVM> builder)
        {
            builder
           .HasOne(p => p.Obra)
           .WithMany()
           .HasForeignKey(p => p.IdObra)
           .OnDelete(DeleteBehavior.Restrict);

           builder
          .HasOne(p => p.Material)
          .WithMany()
          .HasForeignKey(p => p.IdMaterial)
          .OnDelete(DeleteBehavior.Restrict);


            builder
 .HasOne(p => p.Disciplina)
 .WithMany()
 .HasForeignKey(p => p.IdDisciplina)
 .OnDelete(DeleteBehavior.Restrict);

            builder
.HasOne(p => p.TipoOC)
.WithMany()
.HasForeignKey(p => p.IdTipoOC)
.OnDelete(DeleteBehavior.Restrict);


            builder
 .HasOne(p => p.Fornecedor)
 .WithMany()
 .HasForeignKey(p => p.IdFornecedor)
 .OnDelete(DeleteBehavior.Restrict);


            builder
 .HasOne(p => p.Transportadora)
 .WithMany()
 .HasForeignKey(p => p.IdTransp)
 .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.CLVMs)
             .WithOne(x => x.LVM)
             .HasForeignKey(x => x.IdLVM);

            builder.Metadata
            .FindNavigation(nameof(CadLVM.CLVMs))!
            .SetField("_clvms");

            builder.Navigation(x => x.CLVMs)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
