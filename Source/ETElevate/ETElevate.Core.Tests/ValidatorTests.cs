using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NUnit.Framework;

namespace ETElevate.Core.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        private const string fieldName = "FirstName";        

        [Test]
        public void WhenFieldIsNotEmpty_Required_ReturnsTrue()
        {
            var dataRecord = new DataRecord();
            dataRecord.SetValue(fieldName, "Michael");

            var validator = new RequiredValidator();
            var isValid = validator.Check(dataRecord.GetValue(fieldName));

            Assert.IsTrue(isValid);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void WhenFieldIsNullOrEmpty_Required_ReturnsFalse(string fieldValue)
        {
            var dataRecord = new DataRecord();
            dataRecord.SetValue(fieldName, fieldValue);
            var validator = new RequiredValidator();
            var isValid = validator.Check(dataRecord.GetValue(fieldName));

            Assert.IsFalse(isValid);
        }

        [Test]
        public void WhenFieldIsNeverSet_Required_ThrowsException()
        {
            var dataRecord = new DataRecord();
            var validator = new RequiredValidator();

            Assert.Throws<ArgumentException>(() => validator.Check(dataRecord.GetValue(fieldName)));
        }

        [Test]
        [TestCase("Michael", 10)]
        [TestCase("Michael", 9)]
        [TestCase("Michael", 8)]
        [TestCase("Michael", 7)]
        [TestCase("", 10)]
        [TestCase(null, 10)]
        public void WhenFieldIsBelowMaxLength_MaxLength_ReturnsTrue(string fieldValue, int maxLength)
        {
            var dataRecord = new DataRecord();
            dataRecord.SetValue(fieldName, fieldValue);

            var validator = new MaxLengthValidator(maxLength);
            var isValid = validator.Check(dataRecord.GetValue(fieldName));

            Assert.IsTrue(isValid);
        }

        [Test]
        [TestCase("Michael", 1)]
        [TestCase("Michael", 6)]
        public void WhenFieldIsAboveMaxLength_MaxLength_ReturnsFalse(string fieldValue, int maxLength)
        {
            var dataRecord = new DataRecord();
            dataRecord.SetValue(fieldName, fieldValue);
            var validator = new MaxLengthValidator(maxLength);
            var isValid = validator.Check(dataRecord.GetValue(fieldName));

            Assert.IsFalse(isValid);
        }

        [Test]
        public void WhenFieldIsNeverSet_MaxLength_ThrowsException()
        {
            var dataRecord = new DataRecord();
            var validator = new MaxLengthValidator(100);

            Assert.Throws<ArgumentException>(() => validator.Check(dataRecord.GetValue(fieldName)));
        }

        [Test]
        [TestCase("BirthDate", "01/01/1980", @"[0-9]{2}\/[0-9]{2}\/[0-9]{4}")]
        [TestCase("BirthDate", "01/01/0000", @"[0-9]{2}\/[0-9]{2}\/[0-9]{4}")]
        [TestCase("BirthDate", "01/01/0001", @"[0-9]{2}\/[0-9]{2}\/[0-9]{4}")]
        [TestCase("BirthDate", "12/31/9999", @"[0-9]{2}\/[0-9]{2}\/[0-9]{4}")]        
        public void WhenFieldIsValidFormat_Format_ReturnsTrue(string fieldName, string fieldValue, string formatRegex)
        {
            var dataRecord = new DataRecord();
            dataRecord.SetValue(fieldName, fieldValue);

            var validator = new FormatValidator(formatRegex);
            var isValid = validator.Check(dataRecord.GetValue(fieldName));

            Assert.IsTrue(isValid);
        }
    }
}
