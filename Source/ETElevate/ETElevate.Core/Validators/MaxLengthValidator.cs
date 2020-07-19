using ETElevate.Core.Validators;

namespace ETElevate.Core
{
    public class MaxLengthValidator : IValidator
    {
        public int MaxCharacterCount { get; private set; }

        public MaxLengthValidator(int maxCharacterCount)
        {
            MaxCharacterCount = maxCharacterCount;
        }

        public ValidationResult Check(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new ValidationResult();
            }

            return value.Length <= MaxCharacterCount
                ? new ValidationResult()
                : new ValidationResult($"Character length of {value.Length} exceeds maximum length of {MaxCharacterCount}");
        }
    }
}
