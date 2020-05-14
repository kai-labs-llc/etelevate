using System.Collections.Generic;

namespace ETElevate.Core
{
    public class DataRecordBuilder
    {
        private Dictionary<int, string> fields = new Dictionary<int, string>();

        public DataRecord Build(IList<string> fieldDataList)
        {
            var dataRecord = new DataRecord();

            foreach (var index in fields.Keys)
            {
                var fieldData = fieldDataList[index];
                var name = fields[index];

                dataRecord.SetValue(name, fieldData);
            }

            return dataRecord;
        }

        public void AddField(int index, string name)
        {
            fields.Add(index, name);            
        }
    }
}
