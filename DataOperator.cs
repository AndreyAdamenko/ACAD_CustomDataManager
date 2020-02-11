using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Security;
using System.Text;
using System.Xml.Serialization;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ACAD_CustomDataManager
{
    /// <summary>
    /// Abstract class for defining operator methods
    /// </summary>
    public abstract class DataOperator
    {
        internal string storagePath;

        internal string fileExtention;

        /// <summary>
        /// Returns string value
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public string GetStringValue(string parameterName)
        {
            try
            {
                string[] files = Directory.GetFiles(storagePath, parameterName + fileExtention);

                string result = File.ReadAllText(files[0], Encoding.GetEncoding(1251));

                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Write string value to storage
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetStringValue(string parameterName, string value)
        {
            try
            {
                string directoryPath = storagePath + "\\";

                if (!IsDirectoryWritable(directoryPath)) return false;

                string fullFilePath = storagePath + "\\" + parameterName + fileExtention;

                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }

                File.WriteAllText(fullFilePath, value, Encoding.GetEncoding(1251));
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns string dictionary
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetDictionary(string parameterName)
        {
            string[] files = Directory.GetFiles(storagePath, "*" + fileExtention);

            foreach (string filePath in files)
            {
                string filename = Path.GetFileNameWithoutExtension(filePath);

                if (filename == parameterName)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(filePath))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(DictionaryItem[]), new XmlRootAttribute() { ElementName = "Items" });

                            Dictionary<string, List<string>> orgDict = ((DictionaryItem[])serializer.Deserialize(sr)).ToDictionary(i => i.key, i => i.value);

                            return orgDict;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Is a directory available to create a file.
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="throwIfFails"></param>
        /// <returns></returns>
        protected bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
        {
            try
            {
                using (FileStream fs = File.Create(
                    Path.Combine(
                        dirPath,
                        Path.GetRandomFileName()
                    ),
                    1,
                    FileOptions.DeleteOnClose)
                )
                { }
                return true;
            }
            catch
            {
                if (throwIfFails)
                    throw;
                else
                    return false;
            }
        }
    }
}
