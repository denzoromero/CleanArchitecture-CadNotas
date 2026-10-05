using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Entities.EntitiesCad.ECadObra;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entities.EntitiesCad
{
    public class EntityClvm : BaseEntity, IAggregateRoot
    {
        public int IdLVM { get; private set; }
        public EntityLVM LVM { get; private set; } = null!;
        public int? CodObra { get; private set; }
        public decimal Item { get; private set; }
        public string Codigo { get; private set; } = string.Empty;
        public string? TipoComponente { get; private set; }
        public string? Certificacao { get; private set; }
        public string? Corrida { get; private set; }
        public string? TMA { get; private set; }
        public string? LVMOrigem { get; private set; }
        public UMedida? UnidadeMedida { get; private set; }
        public decimal? Qtd { get; private set; }
        public int? IdInspetor { get; private set; }
        public DateTime DtInspecao { get; private set; }
        public int? Programacao { get; private set; }
        public ClvmStatus Status { get; private set; }
        public string? Observacao { get; private set; }
        public int PO { get; private set; }
        [Column("idCodigoMaterial")]
        public int? IdCodigoMaterial { get; private set; }
        public EntityMaterialCLVM MaterialCLVM { get; private set; } = null!;

        public int? TransferStatus { get; private set; }

        private EntityClvm() { }

        public EntityClvm(int idLVM,decimal item, string codigo, DateTime dtInspecao, ClvmStatus status, int po, int idMaterialCLVM)
        {
            Guard.Against.NegativeOrZero(idLVM, nameof(idLVM));
            Guard.Against.NegativeOrZero(item, nameof(item));
            Guard.Against.NullOrEmpty(codigo, nameof(codigo));
            Guard.Against.Default(dtInspecao, nameof(dtInspecao));
            Guard.Against.InvalidInput(status,nameof(status),s => Enum.IsDefined(typeof(ClvmStatus), s) && s != ClvmStatus.None);
            Guard.Against.NegativeOrZero(po, nameof(po));
            Guard.Against.NegativeOrZero(idMaterialCLVM, nameof(idMaterialCLVM));

            IdLVM = idLVM;
            Item = item;
            Codigo = codigo;
            DtInspecao = dtInspecao;
            Status = status;
            PO = po;
            IdCodigoMaterial = idMaterialCLVM;
            Ativo = 1;
            TransferStatus = 0;
        }

        public static EntityClvm Create(int idLVM, decimal item, string codigo, DateTime dtInspecao, ClvmStatus status, int po, int idMaterialCLVM,
            int? codObra, string? tipoComponente, string? certificacao, string? corrida, string? tma, string? lvmOrigem, UMedida? umedida, decimal? qtd,
            int? idInspetor, int? programacao, string? observacao)
        {

            return new EntityClvm(idLVM, item, codigo, dtInspecao, status, po, idMaterialCLVM)
            {
                CodObra = codObra,
                TipoComponente = tipoComponente,
                Certificacao = certificacao,
                Corrida = corrida,
                TMA = tma,
                LVMOrigem = lvmOrigem,
                UnidadeMedida = umedida,
                Qtd = qtd,
                IdInspetor = idInspetor,
                Programacao = programacao,
                Observacao = observacao
            };

        }

        public void Update(int idLVM, decimal item, string codigo, DateTime dtInspecao, ClvmStatus status, int po, int idMaterialCLVM,
            int? codObra, string? tipoComponente, string? certificacao, string? corrida, string? tma, string? lvmOrigem, UMedida? umedida, decimal? qtd,
            int? programacao, string? observacao)
        {
            Guard.Against.NegativeOrZero(idLVM, nameof(idLVM));
            Guard.Against.NegativeOrZero(item, nameof(item));
            Guard.Against.NullOrEmpty(codigo, nameof(codigo));
            Guard.Against.Default(dtInspecao, nameof(dtInspecao));
            Guard.Against.InvalidInput(status, nameof(status), s => Enum.IsDefined(typeof(ClvmStatus), s) && s != ClvmStatus.None);
            Guard.Against.NegativeOrZero(po, nameof(po));
            Guard.Against.NegativeOrZero(idMaterialCLVM, nameof(idMaterialCLVM));

            IdLVM = idLVM;
            Item = item;
            Codigo = codigo;
            DtInspecao = dtInspecao;
            Status = status;
            PO = po;
            IdCodigoMaterial = idMaterialCLVM;

            CodObra = codObra;
            TipoComponente = tipoComponente;
            Certificacao = certificacao;
            Corrida = corrida;
            TMA = tma;
            LVMOrigem = lvmOrigem;
            UnidadeMedida = umedida;
            Qtd = qtd;
            Programacao = programacao;
            Observacao = observacao;

        }

        public void Deactivate()
        {
            Ativo = 0;
        }


    }
}
