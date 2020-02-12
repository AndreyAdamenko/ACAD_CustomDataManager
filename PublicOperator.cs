using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ACAD_CustomDataManager
{
    /// <summary>
    /// Operates with public data
    /// </summary>
    public class PublicOperator : DataOperator
    {
        private FileNamesList files = new FileNamesList();

        /// <summary>
        /// Constructor of class
        /// </summary>
        public PublicOperator()
        {
            storagePath = CustomDataManager.settings.publicStoragePath;

            stringParameterFileExtention = CustomDataManager.settings.publicStorageFileExtention;
        }

        /// <summary>
        /// Return the file from public storage by name and add it to list for caching.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public new string GetFile(string fileName)
        {
            string result = base.GetFile(fileName);

            if (result == null)
            {
                result = GetCacheFile(fileName);

                if (result == null)
                {
                    return null;
                }
            }

            files.Add(fileName);

            SaveFileToCache(fileName);

            return result;
        }

        /// <summary>
        /// Returns string value
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public new string GetStringValue(string parameterName)
        {
            try
            {
                string fileName = GetFile(parameterName);

                if (fileName == null)
                {
                    fileName = GetCacheFile(parameterName);

                    if (fileName == null)
                    {
                        return null;
                    }
                }

                string result = File.ReadAllText(fileName, Encoding.GetEncoding(1251));

                return result;
            }
            catch
            {
                return null;
            }
        }

        private string GetCacheFile(string parameterName)
        {
            string fileName = CustomDataManager.GetApplicationPath() + "Cache\\" + parameterName + stringParameterFileExtention;

            if (!File.Exists(fileName)) return null;

            return fileName;
        }

        /// <summary>
        /// Saves used files to local cache for using when public storage is unassessable.
        /// </summary>
        public void SaveAllFilesToCache()
        {
            foreach (string file in files)
            {
                SaveFileToCache(file);
            }
        }

        /// <summary>
        /// Save file to cache for using when public storage is unassessable.
        /// </summary>
        /// <param name="fileName"></param>
        private void SaveFileToCache(string fileName)
        {
            try
            {
                string cacheFileName = Path.GetFileName(fileName);

                string newFile = CustomDataManager.GetApplicationPath() + cacheFileName;

                File.Copy(fileName, newFile, true);
            }
            catch { }
        }
    }
}



