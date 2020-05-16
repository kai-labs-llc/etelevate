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

        public bool Check(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return codeList.Contains(value);
        }
    }
}
