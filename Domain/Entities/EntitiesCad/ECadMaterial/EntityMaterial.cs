using Ardalis.GuardClauses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad.ECadMaterial
{
    public class EntityMaterial : BaseEntity, IAggregateRoot
    {
        public string Material { get; private set; } = string.Empty;

        private EntityMaterial() { }

        public EntityMaterial(string material)
        {
            Material = material;
            Ativo = 1;
        }

        public static EntityMaterial Create(string material)
        {
            Guard.Against.NullOrEmpty(material, nameof(material));

            var entity = new EntityMaterial(material)
            {
                DataRegistro = DateTime.UtcNow
            };

            return entity;

        }

        public void Update(string material)
        {
            Guard.Against.NullOrEmpty(material, nameof(material));

            Material = material;
        }

    }
}
