namespace ETElevate.Core
{
    public class MaxLengthValidator : IValidator
    {
        public int MaxCharacterCount { get; private set; }

        public MaxLengthValidator(int maxCharacterCount)
        {
            MaxCharacterCount = maxCharacterCount;
        }

        public bool Check(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            return value.Length <= MaxCharacterCount;
        }
    }
}
