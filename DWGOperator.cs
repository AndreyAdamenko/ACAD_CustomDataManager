
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAD_CustomDataManager
{
    /// <summary>
    /// Operates with drawing data
    /// </summary>
    public class DWGOperator
    {
        /// <summary>
        /// Search XRecord by name and returns the first his string value
        /// </summary>
        /// <param parameterName="parameterName"></param>
        /// <returns></returns>
        public string GetStringValue(Document doc, string parameterName)
        {
            List<TypedValue> vals = CustomDataManager.XMan.GetXRecord(doc, parameterName);

            if (vals == null) return null;

            foreach (TypedValue tv in vals)
            {
                string result = (string)tv.Value;

                if (result != null && result != "") return tv.Value as string;
            }

            doc.Dispose();

            return null;
        }

        /// <summary>
        /// Write string value to drawing database.
        /// </summary>
        /// <param name="doc">AutoCAD document.</param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public bool SetStringValue(Document doc, string parameterName, string value)
        {
            if (doc == null) return false;
            
            TypedValue tVal = new TypedValue((int)DxfCode.Text, value);

            CustomDataManager.XMan.AddXRecord(doc, new List<TypedValue>() { tVal }, parameterName);

            //doc.Dispose();

            return true;
        }
    }
}
