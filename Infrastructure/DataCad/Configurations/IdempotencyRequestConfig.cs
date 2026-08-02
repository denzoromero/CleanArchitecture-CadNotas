using Domain.Entities.EntitiesCad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataCad.Configurations
{
    public class IdempotencyRequestConfig : IEntityTypeConfiguration<IdempotencyRequest>
    {
        public void Configure(EntityTypeBuilder<IdempotencyRequest> builder)
        {
            builder.HasIndex(x => x.IdempotencyKey).IsUnique();
        }
    }
}
