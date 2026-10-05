using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Entities.EntitiesCad.EObraVSProjeto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad.ECadObra
{
    public class EntityObra : BaseEntity, IAggregateRoot
    {
        public int? OldId { get; private set; }
        public string Obra { get; private set; } = string.Empty;
        public string Cliente { get; private set; } = string.Empty;
        public string? Descricao { get; private set; }
        public string? Contrato { get; private set; }
        public string? Mascara { get; private set; }
        public string? Ultlvm { get; private set; }
        public string? Transferencia { get; private set; }

        private readonly List<ObraVSProjeto> _projetos = [];
        public IReadOnlyCollection<ObraVSProjeto> Projetos => _projetos.AsReadOnly();

        private readonly List<CadProcedimento> _procedimentos = [];
        public IReadOnlyCollection<CadProcedimento> Procedimentos => _procedimentos.AsReadOnly();

        private EntityObra() { }

        public EntityObra(string obra, string cliente)
        {
            Obra = obra;
            Cliente = cliente;

            Ativo = 1;
        }

        public static EntityObra Create(string obra, string cliente,
            string? descricao, string? contrato, string? masraca, string? ultlvm, string? transferencia)
        {
            Guard.Against.NullOrEmpty(obra, nameof(obra));
            Guard.Against.NullOrEmpty(cliente, nameof(cliente));

            var entity = new EntityObra(obra, cliente)
            {
                Descricao = descricao,
                Contrato = contrato,
                Mascara = masraca,
                Ultlvm = ultlvm,
                Transferencia = transferencia,
                DataRegistro = DateTime.UtcNow
            };


            return entity;
        }

        public void Update(string obra, string cliente,
            string? descricao, string? contrato, string? masraca, string? ultlvm, string? transferencia)
        {
            Guard.Against.NullOrEmpty(obra, nameof(obra));
            Guard.Against.NullOrEmpty(cliente, nameof(cliente));

            Obra = obra;
            Cliente = cliente;
            Descricao = descricao;
            Contrato = contrato;
            Mascara = masraca;
            Ultlvm = ultlvm; 
            Transferencia = transferencia;
        }



    }
}
