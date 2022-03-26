using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Aggregetter.Aggre.Application.Exceptions
{
    public sealed class ValidationException : ApplicationException
    {
        public Dictionary<string, string> ValidationErrors { get; set; }

        public ValidationException(IEnumerable<ValidationFailure> errors, string message) : base(message)
        {
            ValidationErrors = new Dictionary<string, string>();
            foreach(var error in errors)
            {
                ValidationErrors.Add(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
