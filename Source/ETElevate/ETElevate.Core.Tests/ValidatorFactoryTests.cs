using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core.Tests
{
    [TestFixture]
    public class ValidatorFactoryTests
    {
        [Test]
        public void CanCreateMaxLengthValidator()
        {
            var validatorSpec = new ValidatorSpec
            {
                Type = ValidatorType.MaxLength,
                Parameters = new List<ValidatorSpecParameter>
                    {
                        new ValidatorSpecParameter { Name = "MaxCharacterCount", Value = 100}
                    }
            };

            var validator = new ValidatorFactory().CreateValidator(validatorSpec);

            Assert.True(validator is MaxLengthValidator);
            Assert.AreEqual(100, (validator as MaxLengthValidator).MaxCharacterCount);
        }

        [Test]
        public void WhenUnknownValidatorType_ThrowsArgumentException()
        {
            var validatorSpec = new ValidatorSpec
            {
                Type = ValidatorType.None
            };

            Assert.Throws<ArgumentException>(() =>
            {
                new ValidatorFactory().CreateValidator(validatorSpec);
            });
        }
    }
}
