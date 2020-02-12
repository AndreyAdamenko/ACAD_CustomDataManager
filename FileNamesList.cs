using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAD_CustomDataManager
{
    class FileNamesList : List<string>
    {
        public new void Add(string fileName)
        {
            if (!Exists(d => d == fileName)) base.Add(fileName);
        }
    }
}
