using ApplicationCore.Common.Validators.Updates;
using FluentValidation;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Update
{
    public class UpdateLVMProcedimentoValidator : UpdateCommonValidator<UpdateLVMProcedimento>
    {
        public UpdateLVMProcedimentoValidator() 
        {
            RuleFor(x => x.IdProcedimento)
              .GreaterThan(0)
              .WithMessage("IdProcedimento is required.");
        }
    }
}
