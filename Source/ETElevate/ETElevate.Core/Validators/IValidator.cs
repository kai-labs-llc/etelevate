using ETElevate.Core.Validators;

namespace ETElevate.Core
{
    public interface IValidator
    {
        ValidationResult Check(string value);
    }
}