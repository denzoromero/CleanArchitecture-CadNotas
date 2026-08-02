using ApplicationCore.Common.Validators.Updates;
using FluentValidation;

namespace ApplicationCore.CadNotasCore.MaterialCLVMs.Commands.Update
{
    public class UpdateMaterialCLVMValidator : UpdateCommonValidator<UpdateMaterialCLVM>
    {
        public UpdateMaterialCLVMValidator()
        {
            RuleFor(x => x.Codigo)
           .NotEmpty()
           .WithMessage("Codigo is required.");
        }
    }
}
