using System.Collections.Generic;

namespace ETElevate.Core
{
    public class FieldSpec
    {
        public string Name { get; set; }
        public IList<ValidatorSpec> ValidatorSpecs { get; private set; }

        public FieldSpec()
        {
            ValidatorSpecs = new List<ValidatorSpec>();
        }
    }
}