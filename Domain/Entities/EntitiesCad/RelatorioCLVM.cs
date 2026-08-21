using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities.EntitiesCad
{
    public class RelatorioCLVM : BaseEntity, IAggregateRoot
    {
        public string IdLVM { get; set; } = string.Empty;
        public RelatorioStatus Status { get; set; }
        public int UserId { get; set; }
        public string? Observacao { get; set; }
        public string? Responsavel { get; set; }
        public int? IdVerificador { get; set; }
        public DateTime? DtVerificar { get; set; }
        public int? IdInspetor { get; set; }
        public DateTime? DtInspetor { get; set; }

        private RelatorioCLVM() { }

        public RelatorioCLVM(string idLVM, RelatorioStatus status, int userId)
        {
            Guard.Against.InvalidInput(status, nameof(status), s => Enum.IsDefined(typeof(RelatorioStatus), s) && s != RelatorioStatus.None);
            Guard.Against.NullOrEmpty(idLVM, nameof(idLVM));
            Guard.Against.NegativeOrZero(userId, nameof(userId));

            IdLVM = idLVM;
            Status = status;
            UserId = userId;
            Ativo = 1;
        }

        public static RelatorioCLVM Create(string idLVM, RelatorioStatus status, int userId,
            string? observacao, string? responsavel)
        {
            return new RelatorioCLVM(idLVM, status, userId)
            {
                Observacao = observacao,
                Responsavel = responsavel
            };
        }

        public void UpdateRelatorio(RelatorioStatus status, string? observacao)
        {
            Status = status;
            Observacao = observacao;
        }


    }
}
