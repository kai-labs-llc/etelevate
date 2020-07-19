using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core.Validators
{
    public class RequiredValidatorFactory : IValidatorFactory
    {
        public IValidator CreateValidator(ValidatorSpec validatorSpec)
        {
            return new RequiredValidator();
        }
    }
}
