using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core.Validators
{
    public class FormatValidatorFactory : IValidatorFactory
    {
        public IValidator CreateValidator(ValidatorSpec validatorSpec)
        {
            var validatorSpecReader = new ValidatorSpecReader();
            string formatRegexPattern = validatorSpecReader.GetStringParameter(validatorSpec, "FormatRegexPattern");
            return new FormatValidator(formatRegexPattern);
        }
    }
}
