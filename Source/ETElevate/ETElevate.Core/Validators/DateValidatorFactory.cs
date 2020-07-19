using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ETElevate.Core.Validators
{
    class DateValidatorFactory : IValidatorFactory
    {
        public IValidator CreateValidator(ValidatorSpec validatorSpec)
        {
            var validatorSpecReader = new ValidatorSpecReader();
            var dateFormat = validatorSpecReader.GetStringParameter(validatorSpec, "DateFormat");
            var culture = new CultureInfo(validatorSpecReader.GetStringParameter(validatorSpec, "CultureName"));
            var minDate = validatorSpecReader.GetDateTimeParameter(validatorSpec, "MinDate");
            var maxDate = validatorSpecReader.GetDateTimeParameter(validatorSpec, "MaxDate");
            return new ValidDateContentValidator(dateFormat, culture, minDate, maxDate);
        }
    }
}
