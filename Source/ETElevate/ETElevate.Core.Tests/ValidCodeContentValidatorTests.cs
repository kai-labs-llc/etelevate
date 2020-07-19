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
        public void WhenCodeIsInList_Check_ReturnsTrue(string fieldValue)
        {
            var validator = new ValidCodeContentValidator(validCodes);            
            var result = validator.Check(fieldValue);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        [TestCase("CodeOne")]
        [TestCase("CodeTwo")]
        [TestCase("CodeThree")]
        [TestCase("CODEFOUR")]
        [TestCase("CODEFIVE")]
        public void WhenCodeIsNotInList_Check_ReturnsFalse(string fieldValue)
        {
            var validator = new ValidCodeContentValidator(validCodes);
            Assert.IsFalse(validator.Check(fieldValue).IsValid);            
        }

        [Test]
        public void WhenValueIsNullAndNotInList_Check_ReturnsFalse()
        {
            var validator = new ValidCodeContentValidator(validCodes);

            Assert.IsFalse(validator.Check(null).IsValid);
        }

        [Test]
        public void WhenValueIsNullAndInList_Check_ReturnsFalse()
        {
            var validCodesWithNullAndEmpty = validCodes.Concat(new[] { null, string.Empty }).ToList();
            var validator = new ValidCodeContentValidator(validCodesWithNullAndEmpty);

            Assert.IsFalse(validator.Check(null).IsValid);
            Assert.IsFalse(validator.Check(string.Empty).IsValid);
        }
    }
}
