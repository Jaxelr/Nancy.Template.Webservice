using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Validation;
using System;
using System.Collections.Generic;

namespace Api.Helpers
{
    public static class ModuleExtensions
    {
        public static void GetHandler<TOut>(this NancyModule module, string path, Func<TOut> handler)
            => module.GetHandler(path, path, handler);

        public static void PostHandler<TOut>(this NancyModule module, string path, Func<TOut> handler)
            => module.PostHandler(path, path, handler);

        public static void DeleteHandler<TOut>(this NancyModule module, string path, Func<TOut> handler)
            => module.DeleteHandler(path, path, handler);

        public static void GetHandler<TOut>(this NancyModule module, string name, string path, Func<TOut> handler)
            => module.Get(path, r => RunHandler(module, handler), name: name);

        public static void PostHandler<TOut>(this NancyModule module, string name, string path, Func<TOut> handler)
            => module.Post(path, r => RunHandler(module, handler), name: name);

        public static void DeleteHandler<TOut>(this NancyModule module, string name, string path, Func<TOut> handler)
            => module.Delete(path, r => RunHandler(module, handler), name: name);

        public static void GetHandler<TIn, TOut>(this NancyModule module, string path, Func<TIn, TOut> handler)
            => module.GetHandler(path, path, handler);

        public static void PostHandler<TIn, TOut>(this NancyModule module, string path, Func<TIn, TOut> handler)
            => module.PostHandler(path, path, handler);

        public static void DeleteHandler<TIn, TOut>(this NancyModule module, string path, Func<TIn, TOut> handler)
            => module.DeleteHandler(path, path, handler);

        public static void GetHandler<TIn, TOut>(this NancyModule module, string name, string path, Func<TIn, TOut> handler)
            => module.Get(path, r => RunHandler(module, handler), name: name);

        public static void PostHandler<TIn, TOut>(this NancyModule module, string name, string path, Func<TIn, TOut> handler)
            => module.Post(path, r => RunHandler(module, handler), name: name);

        public static void DeleteHandler<TIn, TOut>(this NancyModule module, string name, string path, Func<TIn, TOut> handler)
            => module.Delete(path, r => RunHandler(module, handler), name: name);

        public static object RunHandler<TOut>(this NancyModule module, Func<TOut> handler)
        {
            try
            {
                return handler();
            }
            catch (HttpException hEx)
            {
                return module.Negotiate.WithStatusCode(hEx.StatusCode).WithModel(hEx.Content);
            }
        }

        public static object RunHandler<TIn, TOut>(this NancyModule module, Func<TIn, TOut> handler)
        {
            try
            {
                TIn model;
                try
                {
                    model = module.BindAndValidate<TIn>();
                    if (!module.ModelValidationResult.IsValid)
                    {
                        return module.Negotiate.RespondWithValidationFailure(module.ModelValidationResult);
                    }
                }
                catch (ModelBindingException)
                {
                    return module.Negotiate.RespondWithValidationFailure("The Model is not binding to the Request");
                }

                return handler(model);
            }
            catch (HttpException hEx)
            {
                return module.Negotiate.WithStatusCode(hEx.StatusCode).WithModel(hEx.Content);
            }
        }

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
    }

    public class ValidationFailedResponse
    {
        public List<string> Messages { get; set; }

        public ValidationFailedResponse()
        {
        }

        public ValidationFailedResponse(ModelValidationResult validationResult)
        {
            Messages = new List<string>();
            ErrorsToStrings(validationResult);
        }

        public ValidationFailedResponse(string message)
        {
            Messages = new List<string>
            {
                message
            };
        }

        private void ErrorsToStrings(ModelValidationResult validationResult)
        {
            foreach (var errorGroup in validationResult.Errors)
            {
                foreach (var error in errorGroup.Value)
                {
                    Messages.Add(error.ErrorMessage);
                }
            }
        }
    }
}
