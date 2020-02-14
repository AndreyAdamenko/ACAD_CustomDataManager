using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAD_CustomDataManager.Exceptions
{
    public class SettingsNullException : System.Exception
    {
        public SettingsNullException(string message) : base(message)
        {
        }
    }
}
