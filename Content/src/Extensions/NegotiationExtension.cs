using System;
using Nancy.Responses.Negotiation;
using Nancy.Template.WebService.Operations;
using Nancy.Validation;

namespace Nancy.Template.WebService.Extensions
{
    public static class NegotiationExtension
    {
        public static Negotiator RespondWithValidationFailure(this Negotiator negotiate, ModelValidationResult validationResult)
        {
            var model = new ValidationFailedResponse(validationResult);

            return negotiate
                .WithModel(model)
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static object RespondWithValidationFailure(this Negotiator negotiate, string message)
        {
            var model = new ValidationFailedResponse(message);

            return negotiate
                .WithModel(model)
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static Negotiator RespondWithFailure(this Negotiator negotiate, Exception exception)
        {
            var model = new FailedResponse(exception);

            return negotiate
                .WithModel(model)
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }
    }
}
