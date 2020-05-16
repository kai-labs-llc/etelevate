using System;

namespace ETElevate.Core
{
    public class FileReaderFactory
    {
        public IFileReader CreateFileReader(FileSpec fileSpec)
        {
            switch (fileSpec.FileType)
            {
                case FileType.CommaSeparatedValues:
                    return new CommaSeparatedValuesReader(fileSpec.FirstLineIsColumnHeaders, CreateDataRecordBuilder(fileSpec));
                default:
                    throw new ArgumentException($"Unable to construct reader for FileType: {fileSpec.FileType}");
            }            
        }

        private DataRecordBuilder CreateDataRecordBuilder(FileSpec fileSpec)
        {
            var dataRecordBuilder = new DataRecordBuilder();

            for (int i = 0; i < fileSpec.FieldSpecs.Count; i++)
            {
                var fieldSpec = fileSpec.FieldSpecs[i];
                dataRecordBuilder.AddField(i, fieldSpec.Name);
            }

            return dataRecordBuilder;
        }
    }
}
