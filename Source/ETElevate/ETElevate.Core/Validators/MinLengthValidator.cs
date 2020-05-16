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

        public bool Check(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            return value.Length >= MinCharacterCount;
        }
    }
}
