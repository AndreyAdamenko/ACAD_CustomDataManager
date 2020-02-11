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
    /// <summary>
    /// Controls the placement of custom data.
    /// </summary>
    public class CustomDataManager
    {
        const string companyName = "KITNG";

        const string applicationName = "CustomDataManager";

        internal static XRecordManager XMan;

        internal static Settings settings;

        /// <summary>
        /// Name of settings file
        /// </summary>
        public static string settingsFileName = "Settings.xml";


        /// <summary>
        /// Provides methods for working with local data.
        /// </summary>
        public LocalOperator Local;

        /// <summary>
        /// Provides methods for working with public data.
        /// </summary>
        public PublicOperator Public;

        /// <summary>
        /// Provides methods for working with drawing data.
        /// </summary>
        public DWGOperator DWG;

        /// <summary>
        /// Constructor of class
        /// </summary>
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

        /// <summary>
        /// Returns an application folder path.
        /// </summary>
        /// <returns></returns>
        internal static string GetApplicationPath()
        {
            string localAppDataPath = Environment.GetEnvironmentVariable("LOCALAPPDATA", EnvironmentVariableTarget.Process);

            string fullSettingsFilePath = localAppDataPath + "\\" + companyName + "\\" + applicationName + "\\";

            return fullSettingsFilePath;
        }
    }

    /// <summary>
    /// Type for storage application settings data.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Name of company.
        /// </summary>
        public string companyName = "";

        /// <summary>
        /// Name of application.
        /// </summary>
        public string commonStorageName = "";

        /// <summary>
        /// Prefix for records storage in drawing.
        /// </summary>
        public string recordsPrefix = "";

        /// <summary>
        /// File extention for local data files.
        /// </summary>
        public string localStorageFileExtention = "";

        /// <summary>
        /// Directory for storage public data files.
        /// </summary>
        public string publicStoragePath = "";

        /// <summary>
        /// File extention for public data files.
        /// </summary>
        public string publicStorageFileExtention = "";
    }
}
