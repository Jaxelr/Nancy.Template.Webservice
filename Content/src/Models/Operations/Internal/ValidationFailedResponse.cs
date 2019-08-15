using Nancy.Validation;
using System.Collections.Generic;

namespace Nancy.Template.WebService.Models.Operations
{
    public class ValidationFailedResponse
    {
        public IList<string> Messages { get; set; }

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
