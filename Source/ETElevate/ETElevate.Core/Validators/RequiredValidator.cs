using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core
{
    public class RequiredValidator : IValidator
    {
        public bool Check(string value)
        {   
            return !string.IsNullOrEmpty(value);
        }
    }
}
