using System.Collections.Generic;

namespace ETElevate.Core
{
    public class FileSpec
    {   
        public FileType FileType { get; set; }
        public bool FirstLineIsColumnHeaders { get; set; }
        public IList<FieldSpec> FieldSpecs { get; set; }

        public FileSpec()
        {
            FieldSpecs = new List<FieldSpec>();
        }
    }
}
