using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ETElevate.Core.Tests
{
    [TestFixture]
    public class FileSpecTests
    {
        [Test]
        public void CanBuildFileSpecWithFields()
        {
            var fileSpec = new FileSpec();
            fileSpec.FileType = FileType.CommaSeparatedValues;
            fileSpec.FirstLineIsColumnHeaders = true;

            var firstNameFieldSpec = new FieldSpec();            
            firstNameFieldSpec.Name = "First Name";
            
            firstNameFieldSpec.ValidatorSpecs.Add(
                new ValidatorSpec 
                { 
                    Type = ValidatorType.Required 
                });

            firstNameFieldSpec.ValidatorSpecs.Add(
                new ValidatorSpec 
                { 
                    Type = ValidatorType.MaxLength,
                    Parameters = new List<ValidatorSpecParameter>
                    {
                        new ValidatorSpecParameter { Name = "MaxCharacterCount", Value = 100}
                    }
                });

            fileSpec.FieldSpecs.Add(firstNameFieldSpec);

            var createdFieldSpec = fileSpec.FieldSpecs.SingleOrDefault(fs => fs.Name == "First Name");
            Assert.IsTrue(createdFieldSpec != null);
            Assert.IsTrue(createdFieldSpec.ValidatorSpecs.Any(vs => vs.Type == ValidatorType.Required));
            Assert.IsTrue(createdFieldSpec.ValidatorSpecs.Any(vs => vs.Type == ValidatorType.MaxLength));
        }
    }
}
