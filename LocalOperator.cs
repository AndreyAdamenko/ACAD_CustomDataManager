using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ACAD_CustomDataManager
{
    public class LocalOperator : DataOperator
    {
        public LocalOperator()
        {
            storagePath = CustomDataManager.GetApplicationPath();

            fileExtention = CustomDataManager.settings.localStorageFileExtention;
        }
    }
}
