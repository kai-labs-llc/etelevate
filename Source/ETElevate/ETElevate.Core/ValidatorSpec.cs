using System.Collections.Generic;

namespace ETElevate.Core
{
    public class ValidatorSpec
    {
        public ValidatorType Type { get; set; }
        public IList<ValidatorSpecParameter> Parameters { get; set; }

        public ValidatorSpec()
        {
            Parameters = new List<ValidatorSpecParameter>();
        }
    }
}