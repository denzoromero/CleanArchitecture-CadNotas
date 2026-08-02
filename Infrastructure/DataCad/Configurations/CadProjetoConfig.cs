using Domain.Entities.EntitiesCad.ECadProjeto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataCad.Configurations
{
    public class CadProjetoConfig : IEntityTypeConfiguration<CadProjeto>
    {
        public void Configure(EntityTypeBuilder<CadProjeto> builder)
        {
            builder.HasMany(x => x.Obras)
                    .WithOne(x => x.Projeto)
                    .HasForeignKey(x => x.IdProjeto);

            builder.Navigation(x => x.Obras)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
