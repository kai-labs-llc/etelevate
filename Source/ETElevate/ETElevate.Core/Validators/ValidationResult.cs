using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ETElevate.Core.Validators
{
    public class ValidationResult
    {
        public string ErrorMessage { get; set; }
        public bool IsValid { get { return string.IsNullOrEmpty(ErrorMessage); } }

        public ValidationResult()
        {
        }

        public ValidationResult(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentException("errorMessage cannot be null or empty.");
            }

            ErrorMessage = errorMessage;
        }
    }
}
