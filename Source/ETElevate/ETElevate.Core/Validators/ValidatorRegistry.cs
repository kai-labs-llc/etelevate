using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core.Validators
{
    public class ValidatorRegistry
    {
        private Dictionary<string, IValidatorFactory> validatorFactories = new Dictionary<string, IValidatorFactory>();
        
        public void Register(string validatorTypeName, IValidatorFactory validatorFactory)
        {
            if (validatorFactories.ContainsKey(validatorTypeName))
            {
                throw new ArgumentException($"A registration already exists for validatorTypeName {validatorTypeName}.");
            }

            validatorFactories[validatorTypeName] = validatorFactory;
        }

        public IValidator CreateValidator(ValidatorSpec validatorSpec)
        {
            if (!validatorFactories.ContainsKey(validatorSpec.Type))
            {
                throw new ArgumentException($"A registration validatorTypeName {validatorSpec.Type} does not exist.");
            }

            return validatorFactories[validatorSpec.Type].CreateValidator(validatorSpec);
        }
    }
}
