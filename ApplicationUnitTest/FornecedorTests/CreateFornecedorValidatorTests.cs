using ApplicationCore.CadNotasCore.Fornecedors.Commands.Create;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationUnitTest.FornecedorTests
{
    public class CreateFornecedorValidatorTests
    {
        private readonly CreateFornecedorValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_IdempotencyKey_Is_Empty()
        {
            var command = new CreateFornecedor
            {
                IdempotencyKey = Guid.Empty
            };
 
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.IdempotencyKey);
        }

        [Fact]
        public void Should_Not_Have_Error_When_IdempotencyKey_Is_Valid()
        {
            var command = new CreateFornecedor
            {
                IdempotencyKey = Guid.NewGuid()
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.IdempotencyKey);
        }


    }
}
