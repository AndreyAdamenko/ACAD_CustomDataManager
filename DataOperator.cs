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
    public abstract class DataOperator
    {
        internal string storagePath;

        internal string fileExtention;

        public string GetStringValue(string parameterName)
        {
            string[] files = Directory.GetFiles(storagePath, "*" + fileExtention);

            foreach (string filePath in files)
            {
                string filename = Path.GetFileNameWithoutExtension(filePath);

                if (filename == parameterName)
                {
                    try
                    {
                        string result = File.ReadAllText(filePath, Encoding.GetEncoding(1251));

                        return result;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;
        }

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
                            
                            Dictionary<string, List<string>> orgDict = ((DictionaryItem[])serializer.Deserialize(sr)).ToDictionary(i => i.id, i => i.value);

                            return orgDict;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        public bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
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
