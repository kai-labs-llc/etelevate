using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core.Validators
{
    public class MinLengthValidatorFactory : IValidatorFactory
    {
        public IValidator CreateValidator(ValidatorSpec validatorSpec)
        {
            var validatorSpecReader = new ValidatorSpecReader();
            var minCharacterCount = validatorSpecReader.GetIntParameter(validatorSpec, "MinCharacterCount");
            return new MinLengthValidator(minCharacterCount);
        }
    }
}
