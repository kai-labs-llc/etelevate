using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ETElevate.Core.Tests
{
    [TestFixture]
    public class FileSpecTests
    {
        private FileSpec communityLabsFileSpec;

        [SetUp]
        public void SetUp()
        {
            var fileSpec = new FileSpec();
            fileSpec.FileType = FileType.CommaSeparatedValues;
            fileSpec.FirstLineIsColumnHeaders = true;

            // First Name Field Spec
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

            // Last Name Field Spec
            var lastNameFieldSpec = new FieldSpec();
            lastNameFieldSpec.Name = "Last Name";

            lastNameFieldSpec.ValidatorSpecs.Add(new ValidatorSpec { Type = ValidatorType.Required });
            lastNameFieldSpec.ValidatorSpecs.Add(
                new ValidatorSpec
                {
                    Type = ValidatorType.MaxLength,
                    Parameters = new List<ValidatorSpecParameter>
                    {
                        new ValidatorSpecParameter { Name = "MaxCharacterCount", Value = 100}
                    }
                });

            fileSpec.FieldSpecs.Add(lastNameFieldSpec);

            // Date of Birth Field Spec
            var dateOfBirth = new FieldSpec();
            dateOfBirth.Name = "Date of Birth";
            dateOfBirth.ValidatorSpecs.Add(new ValidatorSpec { Type = ValidatorType.Required });
            dateOfBirth.ValidatorSpecs.Add(
                new ValidatorSpec
                {
                    Type = ValidatorType.Format,
                    Parameters = new List<ValidatorSpecParameter>
                    {
                        new ValidatorSpecParameter{ Name = "FormatRegexPattern", Value = @"\d{2}/\d{2}/\{d}4" }
                    }
                });

            fileSpec.FieldSpecs.Add(dateOfBirth);

            // Patient ID Field Spec
            var patientIdSpec = new FieldSpec();
            patientIdSpec.Name = "Patient ID";

            patientIdSpec.ValidatorSpecs.Add(new ValidatorSpec { Type = ValidatorType.Required });
            patientIdSpec.ValidatorSpecs.Add(
                new ValidatorSpec
                {
                    Type = ValidatorType.MaxLength,
                    Parameters = new List<ValidatorSpecParameter>
                    {
                        new ValidatorSpecParameter { Name = "MaxCharacterCount", Value = 15}
                    }
                });
            patientIdSpec.ValidatorSpecs.Add(
                new ValidatorSpec
                {
                    Type = ValidatorType.Format,
                    Parameters = new List<ValidatorSpecParameter>
                    {
                        // All of our patient IDs start with 899 followed by 12 digits.
                        // This is a totally arbitrary made up format, but this type of thing does
                        // show up in real systems.
                        new ValidatorSpecParameter{ Name = "FormatRegexPattern", Value = @"899\d{12}" }
                    }
                });

            fileSpec.FieldSpecs.Add(patientIdSpec);

            // Observation Date
            var obsDateSpec = new FieldSpec();
            obsDateSpec.Name = "Observation Date";
            obsDateSpec.ValidatorSpecs.Add(new ValidatorSpec { Type = ValidatorType.Required });           
            obsDateSpec.ValidatorSpecs.Add(
                new ValidatorSpec
                {
                    Type = ValidatorType.Format,
                    Parameters = new List<ValidatorSpecParameter>
                    {
                        new ValidatorSpecParameter{ Name = "FormatRegexPattern", Value = @"\d{2}/\d{2}/\{d}4" }
                    }
                });

            fileSpec.FieldSpecs.Add(obsDateSpec);

            // Result Date
            var resultDateSpec = new FieldSpec();
            resultDateSpec.Name = "Result Date";
            resultDateSpec.ValidatorSpecs.Add(new ValidatorSpec { Type = ValidatorType.Required });            
            resultDateSpec.ValidatorSpecs.Add(
                new ValidatorSpec
                {
                    Type = ValidatorType.Format,
                    Parameters = new List<ValidatorSpecParameter>
                    {
                        new ValidatorSpecParameter{ Name = "FormatRegexPattern", Value = @"\d{2}/\d{2}/\{d}4" }
                    }
                });

            // We will implement the comparison rule against Observation Date later.
            fileSpec.FieldSpecs.Add(resultDateSpec);

            // Lab Test Type CPT Code
            var labTestTypeSpec = new FieldSpec();
            labTestTypeSpec.Name = "Lab Test Type";

            labTestTypeSpec.ValidatorSpecs.Add(new ValidatorSpec { Type = ValidatorType.Required });
            labTestTypeSpec.ValidatorSpecs.Add(
                new ValidatorSpec
                {
                    Type = ValidatorType.MaxLength,
                    Parameters = new List<ValidatorSpecParameter>
                    {
                        new ValidatorSpecParameter { Name = "MaxCharacterCount", Value = 15}
                    }
                });
                
            labTestTypeSpec.ValidatorSpecs.Add(
                new ValidatorSpec
                {
                    Type = ValidatorType.Code,
                    Parameters = new List<ValidatorSpecParameter>
                    {
                        new ValidatorSpecParameter { Name = "CodeList", Value = "83036,82947,83721,83718,82465" }
                    }
                });

            fileSpec.FieldSpecs.Add(labTestTypeSpec);

            // Result Value
            var resultValueSpec = new FieldSpec();
            resultValueSpec.Name = "Result Value";
            resultValueSpec.ValidatorSpecs.Add(new ValidatorSpec { Type = ValidatorType.Required });
            resultValueSpec.ValidatorSpecs.Add(
                new ValidatorSpec
                {
                    Type = ValidatorType.MaxLength,
                    Parameters = new List<ValidatorSpecParameter>
                    {
                        new ValidatorSpecParameter{ Name = "MaxCharacterCount", Value = 50 }
                    }
                });

            fileSpec.FieldSpecs.Add(resultValueSpec);

            communityLabsFileSpec = fileSpec;
        }

        [Test]
        public void CanBuildFileSpecWithFields()
        {
            var createdFieldSpec = communityLabsFileSpec.FieldSpecs.SingleOrDefault(fs => fs.Name == "First Name");
            Assert.IsTrue(createdFieldSpec != null);
            Assert.IsTrue(createdFieldSpec.ValidatorSpecs.Any(vs => vs.Type == ValidatorType.Required));
            Assert.IsTrue(createdFieldSpec.ValidatorSpecs.Any(vs => vs.Type == ValidatorType.MaxLength));
        }

        [Test]
        public void CanSerializeAndDeserializeWithJson()
        {
            var json = JsonConvert.SerializeObject(communityLabsFileSpec);
            var deserializedFileSpec = JsonConvert.DeserializeObject<FileSpec>(json);

            Assert.AreEqual(communityLabsFileSpec.FileType, deserializedFileSpec.FileType);
            Assert.AreEqual(communityLabsFileSpec.FirstLineIsColumnHeaders, deserializedFileSpec.FirstLineIsColumnHeaders);
            Assert.AreEqual(communityLabsFileSpec.FieldSpecs.Count, deserializedFileSpec.FieldSpecs.Count);

            for (var i = 0; i < communityLabsFileSpec.FieldSpecs.Count; i++)
            {
                Assert.AreEqual(communityLabsFileSpec.FieldSpecs[i].Name, deserializedFileSpec.FieldSpecs[i].Name);
                
                for (var j = 0; j < communityLabsFileSpec.FieldSpecs[i].ValidatorSpecs.Count; j++)
                {
                    Assert.AreEqual(communityLabsFileSpec.FieldSpecs[i].ValidatorSpecs[j].Parameters.Count, deserializedFileSpec.FieldSpecs[i].ValidatorSpecs[j].Parameters.Count);

                    for (var k = 0; k < communityLabsFileSpec.FieldSpecs[i].ValidatorSpecs[j].Parameters.Count; k++)
                    {
                        Assert.AreEqual(communityLabsFileSpec.FieldSpecs[i].ValidatorSpecs[j].Parameters[k].Name,
                            deserializedFileSpec.FieldSpecs[i].ValidatorSpecs[j].Parameters[k].Name);

                        Assert.AreEqual(communityLabsFileSpec.FieldSpecs[i].ValidatorSpecs[j].Parameters[k].Value,
                            deserializedFileSpec.FieldSpecs[i].ValidatorSpecs[j].Parameters[k].Value);
                    }
                }
            }
        }
    }
}
