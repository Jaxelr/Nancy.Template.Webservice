using System;
using System.Threading.Tasks;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Template.WebService.Models.Operations;
using Nancy.Validation;
using Nancy;

namespace Nancy.Template.WebService.Extensions
{
    public static class ModuleExtensions
    {
        private static string ModelBindingErrorMessage => "The model is not binding to the request";

        public static void GetHandler<TOut>(this NancyModule module, string path, Func<TOut> handler)
            => module.GetHandler(path, path, handler);

        public static void PostHandler<TOut>(this NancyModule module, string path, Func<TOut> handler)
            => module.PostHandler(path, path, handler);

        public static void DeleteHandler<TOut>(this NancyModule module, string path, Func<TOut> handler)
            => module.DeleteHandler(path, path, handler);

        public static void GetHandler<TOut>(this NancyModule module, string name, string path, Func<TOut> handler)
            => module.Get(path, _ => RunHandler(module, handler), name: name);

        public static void PostHandler<TOut>(this NancyModule module, string name, string path, Func<TOut> handler)
            => module.Post(path, _ => RunHandler(module, handler), name: name);

        public static void DeleteHandler<TOut>(this NancyModule module, string name, string path, Func<TOut> handler)
            => module.Delete(path, _ => RunHandler(module, handler), name: name);

        public static void GetHandler<TIn, TOut>(this NancyModule module, string path, Func<TIn, TOut> handler)
            => module.GetHandler(path, path, handler);

        public static void PostHandler<TIn, TOut>(this NancyModule module, string path, Func<TIn, TOut> handler)
            => module.PostHandler(path, path, handler);

        public static void DeleteHandler<TIn, TOut>(this NancyModule module, string path, Func<TIn, TOut> handler)
            => module.DeleteHandler(path, path, handler);

        public static void GetHandler<TIn, TOut>(this NancyModule module, string name, string path, Func<TIn, TOut> handler)
            => module.Get(path, _ => RunHandler(module, handler), name: name);

        public static void PostHandler<TIn, TOut>(this NancyModule module, string name, string path, Func<TIn, TOut> handler)
            => module.Post(path, _ => RunHandler(module, handler), name: name);

        public static void DeleteHandler<TIn, TOut>(this NancyModule module, string name, string path, Func<TIn, TOut> handler)
            => module.Delete(path, _ => RunHandler(module, handler), name: name);

        public static void GetHandler<TIn>(this NancyModule module, string name, string path, Func<TIn, Task<object>> handler)
            => module.Get(path, _ => RunHandlerAsync(module, handler), name: name);

        public static void PostHandler<TIn>(this NancyModule module, string name, string path, Func<TIn, Task<object>> handler)
            => module.Post(path, _ => RunHandlerAsync(module, handler), name: name);

        public static void DeleteHandler<TIn>(this NancyModule module, string name, string path, Func<TIn, Task<object>> handler)
            => module.Delete(path, _ => RunHandlerAsync(module, handler), name: name);

        public static object RunHandler<TOut>(this NancyModule module, Func<TOut> handler)
        {
            try
            {
                return handler();
            }
            catch (Exception Ex)
            {
                return module.Negotiate.RespondWithFailure(Ex);
            }
        }

        public static async Task<object> RunHandlerAsync(this NancyModule module, Func<Task<object>> handler)
        {
            try
            {
                return await handler().ConfigureAwait(false);
            }
            catch (Exception Ex)
            {
                return module.Negotiate.RespondWithFailure(Ex);
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
                    return module.Negotiate.RespondWithValidationFailure(ModelBindingErrorMessage);
                }

                return handler(model);
            }
            catch (Exception Ex)
            {
                return module.Negotiate.RespondWithFailure(Ex);
            }
        }

        public static async Task<object> RunHandlerAsync<TIn>(this NancyModule module, Func<TIn, Task<object>> handler)
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
                    return module.Negotiate.RespondWithValidationFailure(ModelBindingErrorMessage);
                }

                return await handler(model).ConfigureAwait(false);
            }
            catch (Exception Ex)
            {
                return module.Negotiate.RespondWithFailure(Ex);
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

        public static Negotiator RespondWithFailure(this Negotiator negotiate, Exception exception)
        {
            var model = new FailedResponse(exception);

            return negotiate
                .WithModel(model)
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }
    }
}
