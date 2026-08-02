using ApplicationCore.Common.Validators.Creates;
using FluentValidation;

namespace ApplicationCore.CadNotasCore.TipoOCs.Commands.Create
{
    public class CreateTipoOCValidator : CreateCommonValidator<CreateTipoOC>
    {
        public CreateTipoOCValidator()
        {
            RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome is required.");
        }
    }
}
