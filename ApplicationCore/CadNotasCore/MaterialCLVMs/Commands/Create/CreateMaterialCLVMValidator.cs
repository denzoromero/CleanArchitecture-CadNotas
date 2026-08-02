using ApplicationCore.Common.Validators.Creates;
using FluentValidation;

namespace ApplicationCore.CadNotasCore.MaterialCLVMs.Commands.Create
{
    public class CreateMaterialCLVMValidator : CreateCommonValidator<CreateMaterialCLVM>
    {
        public CreateMaterialCLVMValidator()
        {
            RuleFor(x => x.Codigo)
            .NotEmpty()
            .WithMessage("Codigo is required.");
        }
    }
}
