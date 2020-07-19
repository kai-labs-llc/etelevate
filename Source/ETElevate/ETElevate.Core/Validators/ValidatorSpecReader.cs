using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ETElevate.Core.Validators
{
    public class ValidatorSpecReader
    {
        public ValidatorSpecParameter GetParameterValue(ValidatorSpec validatorSpec, string parameterName)
        {
            var param = validatorSpec.Parameters.SingleOrDefault(p => p.Name == parameterName);

            if (param == null)
            {
                throw new ArgumentException($"Cannot find ValidatorSpecParameter with Name: {parameterName}");
            }

            return param;
        }

        public string GetStringParameter(ValidatorSpec validatorSpec, string parameterName)
        {
            var param = GetParameterValue(validatorSpec, parameterName);
            return (string)param.Value;
        }

        public int GetIntParameter(ValidatorSpec validatorSpec, string parameterName)
        {
            var param = GetParameterValue(validatorSpec, parameterName);
            return Convert.ToInt32(param.Value);
        }

        public DateTime GetDateTimeParameter(ValidatorSpec validatorSpec, string parameterName)
        {
            var param = GetStringParameter(validatorSpec, parameterName);
            return DateTime.ParseExact(param, "MM/dd/yyyy", new CultureInfo("en-us"), DateTimeStyles.None);
        }
    }
}
