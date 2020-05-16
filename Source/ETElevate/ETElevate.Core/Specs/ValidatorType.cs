using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ETElevate.Core
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ValidatorType
    {
        None = 0,
        Required = 1,
        MinLength = 2,
        MaxLength = 3,
        Format = 4,
        Content = 5,
        Code = 6,
        Date = 7
    }
}
