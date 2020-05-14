using System.Collections.Generic;
using System.IO;

namespace ETElevate.Core
{
    public class CommaSeparatedValuesReader : IFileReader
    {
        private readonly bool firstLineIsColumnHeaders;
        private readonly DataRecordBuilder dataRecordBuilder;
        private int currentLine = 0;

        public CommaSeparatedValuesReader(bool firstLineIsColumnHeaders, DataRecordBuilder dataRecordBuilder)
        {
            this.firstLineIsColumnHeaders = firstLineIsColumnHeaders;
            this.dataRecordBuilder = dataRecordBuilder;
        }

        public DataRecord ReadNextDataRecord(StreamReader reader)
        {
            var fields = ReadNextDataLine(reader);
            return dataRecordBuilder.Build(fields);
        }

        private IList<string> ReadNextDataLine(StreamReader reader)
        {
            if (currentLine == 0 && firstLineIsColumnHeaders)
            {
                DiscardHeaderLine(reader);
            }

            var lineData = reader.ReadLine();
            return lineData.Split(",");
        }

        private void DiscardHeaderLine(StreamReader reader)
        {
            reader.ReadLine();
            currentLine++;
        }
    }
}
