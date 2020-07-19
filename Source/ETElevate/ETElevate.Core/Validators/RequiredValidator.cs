using ETElevate.Core.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core
{
    public class RequiredValidator : IValidator
    {
        public ValidationResult Check(string value)
        {
            return !string.IsNullOrEmpty(value)
                ? new ValidationResult()
                : new ValidationResult("Required value is null or empty.");
        }
    }
}
