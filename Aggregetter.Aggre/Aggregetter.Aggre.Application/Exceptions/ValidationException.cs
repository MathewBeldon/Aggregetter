using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Exceptions
{
    public sealed class ValidationException : ApplicationException
    {
        private static readonly string _message = "Validation Error";

        public Dictionary<string, string> ValidationErrors { get; set; }

        public ValidationException(IEnumerable<ValidationFailure> errors) : base(_message)
        {
            ValidationErrors = new Dictionary<string, string>();
            foreach(var error in errors)
            {
                ValidationErrors.Add(error.PropertyName, error.ErrorMessage);
            }
        }

        public ValidationException(string property, string errorMessage)
        {
            ValidationErrors = new Dictionary<string, string>();
            ValidationErrors.Add(property, errorMessage);
        }
    }
}
