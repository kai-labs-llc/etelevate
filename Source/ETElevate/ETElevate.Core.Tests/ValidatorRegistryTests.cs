using ETElevate.Core.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETElevate.Core.Tests
{
    [TestFixture]
    public class ValidatorRegistryTests
    {
        [Test]
        public void CanRegisterAndCreateValidatorWithoutSpecParameters()
        {
            var registry = new ValidatorRegistry();
            registry.Register(ValidatorType.Required, new RequiredValidatorFactory());

            var spec = new ValidatorSpec
            {
                Type = ValidatorType.Required
            };

            var validator = registry.CreateValidator(spec);

            Assert.NotNull(validator);
            Assert.IsTrue(validator is RequiredValidator);
        }

        [Test]
        public void CanRegisterAndCreateValidatorWithSpecParameters()
        {
            var registry = new ValidatorRegistry();
            registry.Register(ValidatorType.MaxLength, new MaxLengthValidatorFactory());

            var spec = new ValidatorSpec
            {
                Type = ValidatorType.MaxLength,
                Parameters =
                {
                    new ValidatorSpecParameter{ Name = "MaxCharacterCount", Value = "5" }
                }
            };

            var validator = registry.CreateValidator(spec);

            Assert.NotNull(validator);
            Assert.IsTrue(validator is MaxLengthValidator);
            Assert.IsFalse(validator.Check("123456").IsValid);
        }
    }
}
