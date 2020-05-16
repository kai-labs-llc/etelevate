using Newtonsoft.Json;
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

        [Test]
        public void CanBuildReaderAndReadDataRecordsFromJsonConfigurationFile()
        {
            var config = File.ReadAllText("TestDataFiles\\CommunityLabsFormat.json");
            var fileSpec = JsonConvert.DeserializeObject<FileSpec>(config);
            var reader = new FileReaderFactory().CreateFileReader(fileSpec);

            using (var stream = new FileStream("TestDataFiles\\CommunityLabResults.csv", FileMode.Open))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var recordCount = 0;

                    while (!streamReader.EndOfStream)
                    {
                        recordCount++;
                        TestContext.Write($"Record: {recordCount}\t");

                        var dataRecord = reader.ReadNextDataRecord(streamReader);
                        TestContext.Write($"First Name: {dataRecord.GetValue("First Name")}\t");
                        TestContext.Write($"Last Name: {dataRecord.GetValue("Last Name")}\t");
                        TestContext.Write($"Date of Birth: {dataRecord.GetValue("Date of Birth")}\t");
                        TestContext.Write($"Patient ID: {dataRecord.GetValue("Patient ID")}\t");
                        TestContext.Write($"Observation Date: {dataRecord.GetValue("Observation Date")}\t");
                        TestContext.Write($"Result Date: {dataRecord.GetValue("Result Date")}\t");
                        TestContext.Write($"Lab Test Type: {dataRecord.GetValue("Lab Test Type")}\t");
                        TestContext.Write($"Result Value: {dataRecord.GetValue("Result Value")}\r\n");
                    }

                    Assert.AreEqual(5, recordCount);
                }
            }
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
