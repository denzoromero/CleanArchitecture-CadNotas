using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Entities.EntitiesCad.EObraVSProjeto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad.ECadProjeto
{
    public class EntityProjeto : BaseEntity, IAggregateRoot
    {
        public string Projeto { get; set; } = string.Empty;
        public string? Cliente { get; set; }

        private readonly List<ObraVSProjeto> _obras = [];

        public IReadOnlyCollection<ObraVSProjeto> Obras => _obras.AsReadOnly();

        private EntityProjeto() { }

        public EntityProjeto(string projeto)
        {
            Projeto = projeto;
            Ativo = 1;
        }

        public static EntityProjeto Create(string projeto, IEnumerable<int> idObras)
        {
            Guard.Against.NullOrEmpty(projeto);

            var entity = new EntityProjeto(projeto);

            foreach (var idObra in idObras)
            {
                entity._obras.Add(new ObraVSProjeto(idObra));
            }

            return entity;
        }

        public void Update(string projeto, IEnumerable<int> idObras)
        {
            Guard.Against.NullOrEmpty(projeto);

            Projeto = projeto;

            var obrasAtuais = _obras.Select(x => x.IdObra).ToHashSet();
            var obrasNovas = idObras.ToHashSet();

            // Remove relationships that no longer exist
            _obras.RemoveAll(x => !obrasNovas.Contains(x.IdObra));

            // Add new relationships
            foreach (var idObra in obrasNovas.Except(obrasAtuais))
            {
                _obras.Add(new ObraVSProjeto(idObra));
            }
        }

        public void UpdateProjeto(string projeto, IEnumerable<int> idObras)
        {
            Guard.Against.NullOrEmpty(projeto);
            Projeto = projeto;
        }

        public void LinkObra(int idObra)
        {
            if (_obras.Any(x => x.IdObra == idObra)) return;
            _obras.Add(new ObraVSProjeto(idObra));
        }

        public void UnlinkObra(int idObra)
        {
            var obra = _obras.FirstOrDefault(x => x.IdObra == idObra);

            if (obra is not null) _obras.Remove(obra);
        }

    }
}
