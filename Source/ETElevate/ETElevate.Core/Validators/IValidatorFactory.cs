using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core.Validators
{
    public interface IValidatorFactory
    {
        IValidator CreateValidator(ValidatorSpec validatorSpec);
    }
}
