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
        const string companyName = "KITNG";

        const string applicationName = "CustomDataManager";

        internal static XRecordManager XMan;

        public static string settingsFileName = "CustomDataManager_settings.xml";

        //internal static string localAppDataPath;

        internal static Settings settings;

        public LocalOperator Local;

        public PublicOperator Public;

        public DWGOperator DWG;

        public CustomDataManager()
        {
            string fullSettingsFilePath = GetApplicationPath() + settingsFileName;

            if (!File.Exists(fullSettingsFilePath))
            {
                throw new Exception("File \"" + fullSettingsFilePath + "\" is not exists!");
            }

            settings = DeserializeSettings(fullSettingsFilePath);

            XMan = new XRecordManager(settings.companyName, settings.commonStorageName, settings.recordsPrefix);

            Local = new LocalOperator();

            Public = new PublicOperator();

            DWG = new DWGOperator();
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
                return null;
            }
        }

        public static string GetApplicationPath()
        {
            string localAppDataPath = Environment.GetEnvironmentVariable("LOCALAPPDATA", EnvironmentVariableTarget.Process);

            string fullSettingsFilePath = localAppDataPath + "\\" + companyName + "\\" + applicationName + "\\";

            return fullSettingsFilePath;
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
