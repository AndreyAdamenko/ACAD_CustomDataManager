using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ACAD_XRecord_Manager;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace ACAD_CustomDataManager
{
    public class CustomDataManager
    {
        private XRecordManager XMan;

        //private bool isInitialized = false;

        public static string settingsFileName = "CustomDataManager_settings.xml";

        private string localAppDataPath;

        private Settings settings;

        public CustomDataManager()
        {
            localAppDataPath = Environment.GetEnvironmentVariable("LOCALAPPDATA", EnvironmentVariableTarget.Process);

            string fullSettingsFilePath = localAppDataPath + "\\" + settingsFileName;

            if (!File.Exists(fullSettingsFilePath))
            {
                throw new Exception("File \"" + fullSettingsFilePath + "\" is not exists!");
            }

            settings = DeserializeSettings(fullSettingsFilePath);

            XMan = new XRecordManager(settings.companyName, settings.commonStorageName, settings.recordsPrefix);
        }

        /// <summary>
        /// Search XRecord by name and returns the first his string value
        /// </summary>
        /// <param parameterName="parameterName"></param>
        /// <returns></returns>
        public string DWGGetStringValue(string parameterName)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            List<TypedValue> vals = XMan.GetXRecord(doc, parameterName);

            doc.Dispose();

            foreach (TypedValue tv in vals)
            {
                string result = (string)tv.Value;

                if (result != null && result != "") return tv.Value as string;
            }

            return null;
        }

        public string LocalGetStringValue(string parameterName)
        {
            string[] files = Directory.GetFiles(localAppDataPath, "*" + settings.localStorageFileExtention);

            foreach (string filePath in files)
            {
                string filename = Path.GetFileNameWithoutExtension(filePath);

                if (filename == parameterName)
                {
                    try
                    {
                        string result = File.ReadAllText(filePath, Encoding.GetEncoding("windows-1251"));

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

        public string PublicGetStringValue(string parameterName)
        {
            string[] files = Directory.GetFiles(settings.publicStoragePath, "*" + settings.publicStorageFileExtention);

            foreach (string filePath in files)
            {
                string filename = Path.GetFileNameWithoutExtension(filePath);

                if (filename == parameterName)
                {
                    try
                    {
                        string result = File.ReadAllText(filePath, Encoding.GetEncoding("windows-1251"));

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

        private static Settings DeserializeSettings(string fileName)
        {
            Settings result = new Settings();

            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Settings));

                    result = (Settings)formatter.Deserialize(fs);

                    return result;
                }
            }
            catch
            {
                return result;
            }
        }
    }

    public class Settings
    {
        public string companyName = "";
        public string commonStorageName = "";
        public string recordsPrefix = "";
        public string localStorageFileExtention = "";
        public string publicStoragePath = "";
        public string publicStorageFileExtention = "";
    }
}
