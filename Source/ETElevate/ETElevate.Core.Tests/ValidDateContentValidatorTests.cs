using System;
using System.Globalization;
using NUnit.Framework;

namespace ETElevate.Core.Tests
{
    [TestFixture]
    public class ValidDateContentValidatorTests
    {
        private const string dateFormat = "MM/dd/yyyy";

        [Test]
        [TestCase("01/01/2020", dateFormat)]
        [TestCase("12/31/2020", dateFormat)]
        public void WhenIsValidDate_CheckValue_ReturnsTrue(string dateString, string dateFormat)
        {
            var dataRecord = new DataRecord();
            dataRecord.SetValue("BirthDate", dateString);

            var validDateValidator = new ValidDateContentValidator(dateFormat, CultureInfo.CurrentCulture);
            var result = validDateValidator.Check(dataRecord.GetValue("BirthDate"));

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        [TestCase("1/1/20", dateFormat)]
        [TestCase("1/1/2020", dateFormat)]
        [TestCase("02/29/2019", dateFormat)]
        [TestCase("02/30/2020", dateFormat)]
        [TestCase("13/30/2020", dateFormat)]
        [TestCase("99/30/9999", dateFormat)]
        public void WhenIsNotValidDate_CheckValue_ReturnsFalse(string dateString, string dateFormat)
        {
            var dataRecord = new DataRecord();
            dataRecord.SetValue("BirthDate", dateString);

            var validDateValidator = new ValidDateContentValidator(dateFormat, CultureInfo.CurrentCulture);
            var result = validDateValidator.Check(dataRecord.GetValue("BirthDate"));

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        [TestCase("01/01/1900", dateFormat, "01/01/1900", "12/31/9999")]
        [TestCase("12/31/2020", dateFormat, "01/01/1900", "12/31/2020")]
        public void WhenDateIsWithinRange_CheckValue_ReturnsTrue(string dateString, string dateFormat, string minDate, string maxDate)
        {
            var dataRecord = new DataRecord();
            dataRecord.SetValue("BirthDate", dateString);

            var validDateValidator = new ValidDateContentValidator(dateFormat, CultureInfo.CurrentCulture, 
                DateTime.ParseExact(minDate, dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None),
                DateTime.ParseExact(maxDate, dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None));

            var result = validDateValidator.Check(dataRecord.GetValue("BirthDate"));

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        [TestCase("12/31/1899", dateFormat, "01/01/1900", "12/31/9999")]
        [TestCase("01/01/1899", dateFormat, "01/01/1900", "12/31/2020")]
        [TestCase("01/01/2021", dateFormat, "01/01/1900", "12/31/2020")]
        [TestCase("01/01/2021", dateFormat, "01/02/2021", "12/31/2021")]
        [TestCase("12/31/2021", dateFormat, "01/01/2022", "12/31/2022")]
        public void WhenDateIsOutsideRange_CheckValue_ReturnsFalse(string dateString, string dateFormat, string minDate, string maxDate)
        {
            var dataRecord = new DataRecord();
            dataRecord.SetValue("BirthDate", dateString);

            var validDateValidator = new ValidDateContentValidator(dateFormat, CultureInfo.CurrentCulture,
                DateTime.ParseExact(minDate, dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None),
                DateTime.ParseExact(maxDate, dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None));

            var result = validDateValidator.Check(dataRecord.GetValue("BirthDate"));

            Assert.IsFalse(result.IsValid);
        }
    }
}
