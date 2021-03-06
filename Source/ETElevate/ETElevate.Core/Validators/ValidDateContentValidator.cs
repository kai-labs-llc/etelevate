﻿using ETElevate.Core.Validators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ETElevate.Core
{
    public class ValidDateContentValidator : IValidator
    {
        private string dateFormat;
        private CultureInfo cultureInfo;
        private readonly DateTime minDate;
        private readonly DateTime maxDate;

        public ValidDateContentValidator(string dateFormat, CultureInfo cultureInfo)
            : this(dateFormat, cultureInfo, DateTime.MinValue, DateTime.MaxValue)
        {
        }

        public ValidDateContentValidator(string dateFormat, CultureInfo cultureInfo, DateTime minDate, DateTime maxDate)
        {
            this.dateFormat = dateFormat;
            this.cultureInfo = cultureInfo;
            this.minDate = minDate;
            this.maxDate = maxDate;
        }

        public ValidationResult Check(string value)
        {
            if (!DateTime.TryParseExact(value,
                dateFormat,
                cultureInfo,
                DateTimeStyles.None,
                out DateTime parsedDate))
            {
                return new ValidationResult("Unable to parse date value");
            }

            return parsedDate >= minDate && parsedDate <= maxDate
                ? new ValidationResult()
                : new ValidationResult("Date is not between {minDate} and {maxDate}");
        }
    }
}
