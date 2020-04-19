using System.Text.RegularExpressions;

namespace ETElevate.Core
{
    public class DataRecordValidator
    {
        public bool CheckRequired(DataRecord record, string fieldName)
        {
            return !string.IsNullOrEmpty(record.GetValue(fieldName));
        }

        public bool CheckMaxLength(DataRecord dataRecord, string fieldName, int maxLength)
        {
            var value = dataRecord.GetValue(fieldName);

            return string.IsNullOrEmpty(value) || value.Length <= maxLength;
        }

        public bool CheckFormat(DataRecord dataRecord, string fieldName, string formatRegex)
        {
            var regex = new Regex(formatRegex);

            return regex.IsMatch(dataRecord.GetValue(fieldName));
        }

        public bool CheckContent(DataRecord dataRecord, string fieldName, ValidDateContentValidator validDateValidator)
        {
            var value = dataRecord.GetValue(fieldName);
            
            return validDateValidator.CheckValue(value);
        }
    }
}
