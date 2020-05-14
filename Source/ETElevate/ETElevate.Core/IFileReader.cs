using System.IO;

namespace ETElevate.Core
{
    public interface IFileReader
    {
        DataRecord ReadNextDataRecord(StreamReader reader);
    }
}
