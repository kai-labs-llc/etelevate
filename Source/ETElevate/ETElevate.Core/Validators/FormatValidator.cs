using ETElevate.Core.Validators;
using System.Text.RegularExpressions;

namespace ETElevate.Core
{
    public class FormatValidator : IValidator
    {
        private readonly string formatRegexPattern;
        private Regex formatRegex;

        public FormatValidator(string formatRegexPattern)
        {
            formatRegex = new Regex(formatRegexPattern);
            this.formatRegexPattern = formatRegexPattern;
        }

        public ValidationResult Check(string value)
        {
            return formatRegex.IsMatch(value)
                ? new ValidationResult()
                : new ValidationResult($"Value does not match format pattern {formatRegexPattern}");
        }
    }
}
