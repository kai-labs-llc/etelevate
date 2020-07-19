using ETElevate.Core.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core.Tests
{
    [TestFixture]
    public class ValidationResultTests
    {
        private const string testErrorMessage = "Test error message";

        [Test]
        public void WhenNoErrorMessage_IsValidIsTrue()
        {
            var result = new ValidationResult();
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void WhenErrorMessage_IsValidIsFalse()
        {
            var result = new ValidationResult(testErrorMessage);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void WhenErrorMessage_ErrorMessageIsSet()
        {
            var result = new ValidationResult(testErrorMessage);
            Assert.AreEqual(testErrorMessage, result.ErrorMessage);            
        }

        [Test]
        public void WhenConstructedWithNullOrEmptyErrorMessage_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => new ValidationResult(null));
            Assert.Throws<ArgumentException>(() => new ValidationResult(string.Empty));
        }
    }
}
