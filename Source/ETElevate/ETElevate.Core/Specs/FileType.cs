using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ETElevate.Core
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FileType
    {
        None = 0,
        CommaSeparatedValues = 1
    }
}
