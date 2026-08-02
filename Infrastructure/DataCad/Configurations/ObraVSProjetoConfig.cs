using Domain.Entities.EntitiesCad.EObraVSProjeto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataCad.Configurations
{
    public class ObraVSProjetoConfig : IEntityTypeConfiguration<ObraVSProjeto>
    {
        public void Configure(EntityTypeBuilder<ObraVSProjeto> builder)
        {
            builder.HasKey(x => new
            {
                x.IdObra,
                x.IdProjeto
            });

            builder.HasOne(x => x.Obra)
       .WithMany(x => x.Projetos)
       .HasForeignKey(x => x.IdObra);

            builder.HasOne(x => x.Projeto)
                   .WithMany(x => x.Obras)
                   .HasForeignKey(x => x.IdProjeto);

        }
    }
}
