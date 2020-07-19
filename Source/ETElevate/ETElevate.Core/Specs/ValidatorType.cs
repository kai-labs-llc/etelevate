using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ETElevate.Core
{   
    public static class ValidatorType
    {
        public const string None = "None";
        public const string Required = "Required";
        public const string MinLength = "MinLength";
        public const string MaxLength = "MaxLength";
        public const string Format = "Format";
        public const string Content = "Content";
        public const string Code = "Code";
        public const string Date = "Date";
    }
}
