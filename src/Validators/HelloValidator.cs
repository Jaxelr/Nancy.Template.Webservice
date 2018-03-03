using Api.Model.Operations;
using FluentValidation;

namespace Api.Validators
{
    public class HelloValidator : AbstractValidator<Hello>
    {
        public HelloValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
