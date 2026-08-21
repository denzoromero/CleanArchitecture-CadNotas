using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Entities.EntitiesCad.ECadFornecedor;
using Domain.Entities.EntitiesCad.ECadMaterial;
using Domain.Entities.EntitiesCad.ECadObra;
using Domain.Entities.EntitiesCad.ECadTransportadora;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.EntitiesCad
{
    public class CadLVM : BaseEntity, IAggregateRoot
    {
        public int? OldId { get; private set; }
        public int IdObra { get; private set; }
        public CadObra Obra { get; private set; } = null!;
        public string? NLVM { get; private set; }
        public string? MascaraLVM { get; private set; }
        public DateTime Data { get; private set; }
        public string? NotaFiscal { get; private set; }
        public string? Documento { get; private set; }
        public int? IdFornecedor { get; private set; }
        public CadFornecedor? Fornecedor { get; private set; }
        public string? OC { get; private set; }
        public string? RM { get; private set; }
        public string? RC { get; private set; }
        public int? IdMaterial { get; private set; }
        public CadMaterial? Material { get; private set; }
        public string? OldMaterial { get; private set; }
        public int? IdTransp { get; private set; }
        public CadTransportadora? Transportadora { get; private set; }
        public string? OldTransp { get; private set; }
        public string? Obs { get; private set; }
        public int? OLDIDOBRA { get; private set; }
        public int? OLDIdFornecedor { get; private set; }
        public int? IdDisciplina { get; private set; }
        public CadDisciplina? Disciplina { get; private set; }
        public int? IdTipoOC { get; private set; }
        public CadTipoOC? TipoOC { get; private set; }
        public int? IdProcedimento { get; private set; }
        public int? IdRelatorioCLVM { get; private set; }
        public RelatorioCLVM? RelatorioCLVM { get; private set; } 
        public decimal? Valor { get; private set; }

        private readonly List<CadClvm> _clvms = [];
        public IReadOnlyCollection<CadClvm> CLVMs => _clvms.AsReadOnly();

        private CadLVM() { }

        public CadLVM(int idObra, DateTime data)
        {
            Guard.Against.NegativeOrZero(idObra, nameof(idObra));
            Guard.Against.Default(data, nameof(data));

            IdObra = idObra;
            Data = data;
            Ativo = 1;
        }

        public static CadLVM Create(int idObra, DateTime data, string? nlvm, string? mascaralvm, string? notafiscal, string? documento,
            int? idFornecedor, string? oc, string? rm, string? rc, int? idMaterial, int? idDisciplina, int? idTipoOC, decimal? valor, string? obs)
        {
            Guard.Against.NegativeOrZero(idObra, nameof(idObra));
            Guard.Against.Default(data, nameof(data));

            return new CadLVM(idObra, data)
            {
                NLVM = nlvm,
                MascaraLVM = mascaralvm,
                NotaFiscal = notafiscal,
                Documento = documento,
                IdFornecedor = idFornecedor,
                OC = oc,
                RM = rm,
                RC = rc,
                IdMaterial = idMaterial,
                IdDisciplina = idDisciplina,
                IdTipoOC = idTipoOC,
                Valor = valor,
                Obs = obs
            };

        }

        public void Update(int idObra, DateTime data, string? nlvm, string? mascaralvm, string? notafiscal, string? documento,
            int? idFornecedor, string? oc, string? rm, string? rc, int? idMaterial, int? idDisciplina, int? idTipoOC, decimal? valor, string? obs)
        {
            Guard.Against.NegativeOrZero(idObra, nameof(idObra));
            Guard.Against.Default(data, nameof(data));

            IdObra = idObra;
            Data = data;
            NLVM = nlvm;
            MascaraLVM = mascaralvm;
            NotaFiscal = notafiscal;
            Documento = documento;
            IdFornecedor = idFornecedor;
            OC = oc;
            RM = rm;
            RC = rc;
            IdMaterial = idMaterial;
            IdDisciplina = idDisciplina;
            IdTipoOC = idTipoOC;
            Valor = valor;
            Obs = obs;
        }

        public void UpdateLVMProcedimento(int idProcedimento)
        {
            IdProcedimento = idProcedimento;
        }

        public void UpdateCLVMRelatorio(int idRelatorio)
        {
            IdRelatorioCLVM = idRelatorio;
        }



    }
}
