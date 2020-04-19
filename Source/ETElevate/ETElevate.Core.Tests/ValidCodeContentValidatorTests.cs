using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ETElevate.Core.Tests
{
    [TestFixture]
    public class ValidCodeContentValidatorTests
    {
        private static readonly List<string> validCodes = new List<string> { "001", "002", "003", "CodeFour", "CodeFive"};

        [Test]
        [TestCase("001")]
        [TestCase("002")]
        [TestCase("003")]
        [TestCase("CodeFour")]
        [TestCase("CodeFive")]
        public void WhenCodeIsInList_CheckValue_ReturnsTrue(string fieldValue)
        {
            var validator = new ValidCodeContentValidator(validCodes);            
            var isValid = validator.CheckValue(fieldValue);

            Assert.IsTrue(isValid);
        }

        [Test]
        [TestCase("CodeOne")]
        [TestCase("CodeTwo")]
        [TestCase("CodeThree")]
        [TestCase("CODEFOUR")]
        [TestCase("CODEFIVE")]
        public void WhenCodeIsNotInList_CheckValue_ReturnsFalse(string fieldValue)
        {
            var validator = new ValidCodeContentValidator(validCodes);
            Assert.IsFalse(validator.CheckValue(fieldValue));            
        }

        [Test]
        public void WhenValueIsNullAndNotInList_CheckValue_ReturnsFalse()
        {
            var validator = new ValidCodeContentValidator(validCodes);

            Assert.IsFalse(validator.CheckValue(null));
        }

        [Test]
        public void WhenValueIsNullAndInList_CheckValue_ReturnsFalse()
        {
            var validCodesWithNullAndEmpty = validCodes.Concat(new[] { null, string.Empty }).ToList();
            var validator = new ValidCodeContentValidator(validCodesWithNullAndEmpty);

            Assert.IsFalse(validator.CheckValue(null));
            Assert.IsFalse(validator.CheckValue(string.Empty));
        }
    }
}
