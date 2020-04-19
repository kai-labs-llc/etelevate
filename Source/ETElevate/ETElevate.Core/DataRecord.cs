using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core
{
    public class DataRecord
    {
        private Dictionary<string, string> fieldValues = new Dictionary<string, string>();

        public void SetValue(string name, string value)
        {
            fieldValues[name] = value;
        }

        public string GetValue(string name)
        {
            if (!fieldValues.ContainsKey(name))
            {
                throw new ArgumentException($"{name} is not a known field name.");
            }

            return fieldValues[name];
        }
    }
}
