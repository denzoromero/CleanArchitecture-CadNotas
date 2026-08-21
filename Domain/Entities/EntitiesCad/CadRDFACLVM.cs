using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad
{
    public class CadRDFACLVM : BaseEntity, IAggregateRoot
    {
        public int? IdObra { get; private set; }
        public int? IdCLVM { get; private set; }
        public int? IdMaterial { get; private set; }
        public string? Descricao { get; private set; }
        public int? Quantidade { get; private set; }
        public int? UnidadeMedida { get; private set; }
        public string? TipoPendencia { get; private set; }
        public string? FaltaDocumental { get; private set; }
        public string? FaltaFisica { get; private set; }
        public int? IdInspetor { get; private set; }
        public DateTime? DtInspecao { get; private set; }
        public string? Obs { get; private set; }
        public string? NoRDFA { get; private set; }
        public string? RDFAStatus { get; private set; }
        public int? NoPendencia { get; private set; }
        public int? QuantidadePendencia { get; private set; }
        public DateTime? DataAtual { get; private set; }
        public string? Resultado { get; private set; }
        public int? GroupLVM { get; private set; }
        public DateTime? DtApprove { get; private set; }
        public int? IdApprovador { get; private set; }
        public DateTime? DtAbertoVerified { get; private set; }
        public int? IdAbertoVerificador { get; private set; }
        public DateTime? DtResponsavelFechamento { get; private set; }
        public int? IdResponsavelFechamento { get; private set; }
        public DateTime? DtVerificacaoFechamento { get; private set; }
        public int? IdVerificacaoFechamento { get; private set; }
    }
}
