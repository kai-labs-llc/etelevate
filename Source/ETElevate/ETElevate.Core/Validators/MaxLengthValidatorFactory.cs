using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core.Validators
{
    public class MaxLengthValidatorFactory : IValidatorFactory
    {
        public IValidator CreateValidator(ValidatorSpec validatorSpec)
        {
            var validatorSpecReader = new ValidatorSpecReader();
            int maxCharacterCount = validatorSpecReader.GetIntParameter(validatorSpec, "MaxCharacterCount");
            return new MaxLengthValidator(maxCharacterCount);
        }
    }
}
