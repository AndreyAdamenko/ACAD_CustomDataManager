using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ACAD_CustomDataManager
{
    /// <summary>
    /// Operates with local data.
    /// </summary>
    public class LocalOperator : DataOperator
    {
        /// <summary>
        /// Constructor of class
        /// </summary>
        public LocalOperator()
        {
            storagePath = CustomDataManager.GetApplicationPath();

            fileExtention = CustomDataManager.settings.localStorageFileExtention;
        }
    }
}
