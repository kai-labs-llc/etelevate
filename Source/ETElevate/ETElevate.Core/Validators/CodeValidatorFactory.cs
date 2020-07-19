using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETElevate.Core.Validators
{
    public class CodeValidatorFactory : IValidatorFactory
    {
        public IValidator CreateValidator(ValidatorSpec validatorSpec)
        {
            var validatorSpecReader = new ValidatorSpecReader();
            var codeListParam = validatorSpecReader.GetStringParameter(validatorSpec, "CodeList");
            var codeList = codeListParam.Split(",").ToList();
            return new ValidCodeContentValidator(codeList);
        }
    }
}
