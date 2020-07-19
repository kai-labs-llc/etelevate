using ETElevate.Core.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core
{
    public class MinLengthValidator : IValidator
    {
        public int MinCharacterCount { get; private set; }

        public MinLengthValidator(int maxCharacterCount)
        {
            MinCharacterCount = maxCharacterCount;
        }

        public ValidationResult Check(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new ValidationResult();
            }

            return value.Length >= MinCharacterCount
                ? new ValidationResult()
                : new ValidationResult($"Character length of {value.Length} is shorter than minimum length of {MinCharacterCount}");
        }
    }
}
