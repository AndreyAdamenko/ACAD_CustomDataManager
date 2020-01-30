
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
    public class DWGOperator : DataOperator
    {
        /// <summary>
        /// Search XRecord by name and returns the first his string value
        /// </summary>
        /// <param parameterName="parameterName"></param>
        /// <returns></returns>
        public new string GetStringValue(string parameterName)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            List<TypedValue> vals = CustomDataManager.XMan.GetXRecord(doc, parameterName);

            doc.Dispose();

            foreach (TypedValue tv in vals)
            {
                string result = (string)tv.Value;

                if (result != null && result != "") return tv.Value as string;
            }

            return null;
        }

        /// <summary>
        /// Write string value to drawing database.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public new void SetStringValue(string parameterName, string value)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            TypedValue tVal = new TypedValue((int)DxfCode.Text, value);

            CustomDataManager.XMan.AddXRecord(doc, new List<TypedValue>() { tVal }, parameterName);

            doc.Dispose();
        }
    }
}
