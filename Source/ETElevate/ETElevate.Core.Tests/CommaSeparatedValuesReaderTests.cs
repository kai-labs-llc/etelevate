using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.IO;

namespace ETElevate.Core.Tests
{
    [TestFixture]
    public class CommaSeparatedValuesReaderTests
    {
        private FileSpec fileSpec;

        [SetUp]
        public void SetUp()
        {
            fileSpec = new FileSpec();
            fileSpec.FileType = FileType.CommaSeparatedValues;
            fileSpec.FirstLineIsColumnHeaders = true;

            var firstNameFieldSpec = new FieldSpec();
            firstNameFieldSpec.Name = "First Name";

            fileSpec.FieldSpecs.Add(firstNameFieldSpec);
        }

        [Test]
        public void CanReadFieldFromStreamWithHeaderRow()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.WriteLine("FIRST NAME,LAST NAME");
            writer.WriteLine("Michael,Bledsoe");
            writer.WriteLine("John,Doe");
            writer.Flush();
            
            stream.Position = 0;

            var reader = new StreamReader(stream);

            var processor = new CommaSeparatedValuesReader(true, CreateTestDataRecordBuilder());
            var dataRecord1 = processor.ReadNextDataRecord(reader);

            Assert.AreEqual("Michael", dataRecord1.GetValue("First Name"));
            Assert.AreEqual("Bledsoe", dataRecord1.GetValue("Last Name"));

            var dataRecord2 = processor.ReadNextDataRecord(reader);
            Assert.AreEqual("John", dataRecord2.GetValue("First Name"));
            Assert.AreEqual("Doe", dataRecord2.GetValue("Last Name"));

            stream.Close();
        }

        [Test]
        public void CanReadFieldFromStreamWithoutHeaderRow()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.WriteLine("Michael,Bledsoe");
            writer.WriteLine("John,Doe");
            writer.Flush();

            stream.Position = 0;

            var reader = new StreamReader(stream);

            var processor = new CommaSeparatedValuesReader(false, CreateTestDataRecordBuilder());
            var dataRecord1 = processor.ReadNextDataRecord(reader);

            Assert.AreEqual("Michael", dataRecord1.GetValue("First Name"));
            Assert.AreEqual("Bledsoe", dataRecord1.GetValue("Last Name"));

            var dataRecord2 = processor.ReadNextDataRecord(reader);
            Assert.AreEqual("John", dataRecord2.GetValue("First Name"));
            Assert.AreEqual("Doe", dataRecord2.GetValue("Last Name"));

            stream.Close();
        }

        private DataRecordBuilder CreateTestDataRecordBuilder()
        {
            var dataRecordBuilder = new DataRecordBuilder();
            dataRecordBuilder.AddField(0, "First Name");
            dataRecordBuilder.AddField(1, "Last Name");

            return dataRecordBuilder;
        }
    }
}
