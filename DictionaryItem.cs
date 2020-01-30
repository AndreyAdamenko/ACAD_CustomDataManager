using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAD_CustomDataManager
{
    /// <summary>
    /// Transition type for dictionary serialization 
    /// </summary>
    public class DictionaryItem
    {
        /// <summary>
        /// Key of dictionary
        /// </summary>
        public string key;

        /// <summary>
        /// Value of dictionary
        /// </summary>
        public List<string> value;
    }
}
