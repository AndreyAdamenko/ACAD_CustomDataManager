using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ACAD_CustomDataManager
{
    public class PublicOperator : DataOperator
    {
        public PublicOperator()
        {
            storagePath = CustomDataManager.settings.publicStoragePath;

            fileExtention = CustomDataManager.settings.publicStorageFileExtention;
        }
    }
}
