using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Entities.EntitiesCad.ECadObra;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad
{
    public class CadProcedimento : BaseEntity, IAggregateRoot
    {
        public int? IdObra { get; private set; }
        public string Procedimento { get; private set; } = string.Empty;
        public string? Revisao { get; private set; }

        public CadObra? Obra { get; private set; }

        private CadProcedimento() { }

        public CadProcedimento(string procedimento)
        {
            Guard.Against.NullOrEmpty(procedimento, nameof(procedimento));

            Procedimento = procedimento;
            Ativo = 1;
        }

        public static CadProcedimento Create(string procedimento, int? idObra, string? revisao)
        {
            Guard.Against.NullOrEmpty(procedimento, nameof(procedimento));

            return new CadProcedimento(procedimento)
            {
                IdObra = idObra,
                Revisao = revisao
            };
           
        }

        public void Update(string procedimento, int? idObra, string? revisao)
        {
            Procedimento = procedimento;
            IdObra = idObra;
            Revisao = revisao;
        }

    }
}
