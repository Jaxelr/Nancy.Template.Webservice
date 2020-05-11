using FluentValidation;
using Nancy.Template.WebService.Operations;

namespace Nancy.Template.WebService.Validators
{
    public class HelloValidator : AbstractValidator<HelloRequest>
    {
        public HelloValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
