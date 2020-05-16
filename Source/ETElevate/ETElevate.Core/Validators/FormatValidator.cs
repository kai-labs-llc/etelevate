using System.Text.RegularExpressions;

namespace ETElevate.Core
{
    public class FormatValidator : IValidator
    {
        private Regex formatRegex;

        public FormatValidator(string formatRegexPattern)
        {
            formatRegex = new Regex(formatRegexPattern);
        }

        public bool Check(string value)
        {
            return formatRegex.IsMatch(value);
        }
    }
}
