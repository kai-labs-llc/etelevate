using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ETElevate.Core.Tests
{
    [TestFixture]
    public class DataRecordTests
    {
        [Test]
        public void CanSetAndGetValue()
        {
            var fieldName = "FirstName";
            var fieldValue = "Michael";

            var dataRecord = new DataRecord();
            dataRecord.SetValue(fieldName, fieldValue);

            Assert.AreEqual(fieldValue, dataRecord.GetValue(fieldName));
        }

        [Test]
        public void CanOverrideValue()
        {
            var fieldName = "FirstName";
            var fieldValue = "Michael";
            var fieldValue2 = "John";

            var dataRecord = new DataRecord();
            dataRecord.SetValue(fieldName, fieldValue);
            dataRecord.SetValue(fieldName, fieldValue2);

            Assert.AreEqual(fieldValue2, dataRecord.GetValue(fieldName));
        }
    }
}
