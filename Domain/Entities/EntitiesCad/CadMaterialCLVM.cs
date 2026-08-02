using Ardalis.GuardClauses;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad
{
    public class CadMaterialCLVM : BaseEntity, IAggregateRoot
    {
        public string Codigo { get; private set; } = string.Empty;
        public string? Especificacao { get; private set; }
        public string? Descricao { get; private set; }
        public string? Diametro1 { get; private set; }
        public string? Diametro2 { get; private set; }
        public string? Comprimento { get; private set; }
        public string? Espessura { get; private set; }
        public string? Largura { get; private set; }
        public string? Peso { get; private set; }
        public string? TipoComponenteMaterial { get; private set; }

        private CadMaterialCLVM() { }

        public CadMaterialCLVM(string codigo)
        {
            Guard.Against.NullOrEmpty(codigo, nameof(codigo));
            Codigo = codigo;
            Ativo = 1;
        }

        public static CadMaterialCLVM Create(string codigo,string? especificacao, string? descricao, string? diametro1, string? diametro2,
            string? comprimento, string? espessura, string? largura, string? peso, string? tipocomponent)
        {
            Guard.Against.NullOrEmpty(codigo, nameof(codigo));

            var entity = new CadMaterialCLVM(codigo)
            {
                Especificacao = especificacao,
                Descricao = descricao,
                Diametro1 = diametro1,
                Diametro2 = diametro2,
                Comprimento = comprimento,
                Espessura = espessura,
                Largura = largura,
                Peso = peso,
                TipoComponenteMaterial = tipocomponent
            };

            return entity;
        }

        public void Update(string codigo, string? especificacao, string? descricao, string? diametro1, string? diametro2,
            string? comprimento, string? espessura, string? largura, string? peso, string? tipocomponent)
        {
            Guard.Against.NullOrEmpty(codigo, nameof(codigo));

            Codigo = codigo;
            Especificacao = especificacao;
            Descricao = descricao;
            Diametro1 = diametro1;
            Diametro2 = diametro2;
            Comprimento = comprimento;
            Espessura = espessura;
            Largura = largura;
            Peso = peso;
            TipoComponenteMaterial = tipocomponent;
        }

    }
}
