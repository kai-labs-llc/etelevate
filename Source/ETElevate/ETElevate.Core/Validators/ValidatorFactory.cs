using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace ETElevate.Core
{
    public class ValidatorFactory
    {
        public IValidator CreateValidator(ValidatorSpec validatorSpec)
        {
            switch (validatorSpec.Type)
            {
                case ValidatorType.Required:
                    return new RequiredValidator();

                case ValidatorType.MinLength:
                    var minCharacterCount = GetIntParameter(validatorSpec, "MinCharacterCount");
                    return new MinLengthValidator(minCharacterCount);

                case ValidatorType.MaxLength:
                    int maxCharacterCount = GetIntParameter(validatorSpec, "MaxCharacterCount");
                    return new MaxLengthValidator(maxCharacterCount);

                case ValidatorType.Format:
                    string formatRegexPattern = GetStringParameter(validatorSpec, "FormatRegexPattern");
                    return new FormatValidator(formatRegexPattern);

                case ValidatorType.Code:
                    var codeListParam = GetStringParameter(validatorSpec, "CodeList");
                    var codeList = codeListParam.Split(",").ToList();
                    return new ValidCodeContentValidator(codeList);

                case ValidatorType.Date:
                    var dateFormat = GetStringParameter(validatorSpec, "DateFormat");
                    var culture = new CultureInfo(GetStringParameter(validatorSpec, "CultureName"));
                    var minDate = GetDateTimeParameter(validatorSpec, "MinDate");
                    var maxDate = GetDateTimeParameter(validatorSpec, "MaxDate");
                    return new ValidDateContentValidator(dateFormat, culture, minDate, maxDate);

                default:
                    throw new ArgumentException($"Unable to create validator instance for type: {validatorSpec.Type}");
            }
        }

        private ValidatorSpecParameter GetParameterValue(ValidatorSpec validatorSpec, string parameterName)
        {
            var param = validatorSpec.Parameters.SingleOrDefault(p => p.Name == parameterName);

            if (param == null)
            {
                throw new ArgumentException($"Cannot find ValidatorSpecParameter with Name: {parameterName}");
            }

            return param;
        }

        private string GetStringParameter(ValidatorSpec validatorSpec, string parameterName)
        {
            var param = GetParameterValue(validatorSpec, parameterName);
            return (string)param.Value;
        }

        private int GetIntParameter(ValidatorSpec validatorSpec, string parameterName)
        {
            var param = GetParameterValue(validatorSpec, parameterName);
            return Convert.ToInt32(param.Value);
        }

        private DateTime GetDateTimeParameter(ValidatorSpec validatorSpec, string parameterName)
        {
            var param = GetStringParameter(validatorSpec, parameterName);
            return DateTime.ParseExact(param, "MM/dd/yyyy", new CultureInfo("en-us"), DateTimeStyles.None);
        }
    }
}
