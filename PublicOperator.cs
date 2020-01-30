using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ACAD_CustomDataManager
{
    /// <summary>
    /// Operates with public data
    /// </summary>
    public class PublicOperator : DataOperator
    {
        /// <summary>
        /// Constructor of class
        /// </summary>
        public PublicOperator()
        {
            storagePath = CustomDataManager.settings.publicStoragePath;

            fileExtention = CustomDataManager.settings.publicStorageFileExtention;
        }
    }
}
