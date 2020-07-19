using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ETElevate.Core
{
    public class ValidatorSpec
    {   
        public string Type { get; set; }
        public IList<ValidatorSpecParameter> Parameters { get; set; }

        public ValidatorSpec()
        {
            Parameters = new List<ValidatorSpecParameter>();
        }
    }
}