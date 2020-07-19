using ETElevate.Core.Validators;
using System.Collections.Generic;

namespace ETElevate.Core
{
    public class ValidCodeContentValidator : IValidator
    {
        private readonly IList<string> codeList;

        public ValidCodeContentValidator(IList<string> codeList)
        {
            this.codeList = codeList;
        }

        public ValidationResult Check(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new ValidationResult("Value cannot be null or empty.");
            }

            return codeList.Contains(value)
                ? new ValidationResult()
                : new ValidationResult($"Valid code list does not contain {value}");
        }
    }
}
