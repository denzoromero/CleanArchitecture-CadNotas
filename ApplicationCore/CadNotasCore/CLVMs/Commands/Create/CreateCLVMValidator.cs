using ApplicationCore.Common.Validators.Creates;
using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Create
{
    public class CreateCLVMValidator : CreateCommonValidator<CreateCLVM>
    {
        public CreateCLVMValidator() 
        {
            RuleFor(x => x.IdLVM).GreaterThan(0).WithMessage("LVM must be greater than zero.");
            RuleFor(x => x.DtInspecao).NotEqual(DateTime.MinValue).WithMessage("DtInspecao is required.");
            RuleFor(x => x.Item).GreaterThan(0m).WithMessage("Item must be greater than zero.");
            RuleFor(x => x.PO).GreaterThan(0).WithMessage("PO must be greater than zero.");
            //RuleFor(x => x.Status).GreaterThan(0).WithMessage("Status must be greater than zero.");
            RuleFor(x => x.Status).IsInEnum().WithMessage("Status should be enum.");
            RuleFor(x => x.Status).NotEqual(ClvmStatus.None).WithMessage("Status is required.");
            RuleFor(x => x.IdMaterialCLVM).GreaterThan(0).WithMessage("IdMaterialCLVM must be greater than zero.");
        }
    }
}
