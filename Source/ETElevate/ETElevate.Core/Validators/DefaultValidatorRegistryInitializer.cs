using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core.Validators
{
    public class DefaultValidatorRegistryInitializer
    {
        public void Initialize(ValidatorRegistry registry)
        {
            registry.Register(ValidatorType.Required, new RequiredValidatorFactory());
            registry.Register(ValidatorType.MinLength, new MinLengthValidatorFactory());
            registry.Register(ValidatorType.MaxLength, new MaxLengthValidatorFactory());
            registry.Register(ValidatorType.Format, new FormatValidatorFactory());
            registry.Register(ValidatorType.Content, new CodeValidatorFactory());
            registry.Register(ValidatorType.Date, new DateValidatorFactory());
        }
    }
}
