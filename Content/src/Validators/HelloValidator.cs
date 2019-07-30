using FluentValidation;
using Nancy.Template.WebService.Models.Operations;

namespace Api.Validators
{
    public class HelloValidator : AbstractValidator<HelloRequest>
    {
        public HelloValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
