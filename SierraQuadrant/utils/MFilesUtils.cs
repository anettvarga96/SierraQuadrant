using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SierraQuadrant.utils
{
    class MFilesUtils
    {
        /// <summary>
        /// Get a dictionary with all files
        /// </summary>
        /// <param name="vault"></param>
        /// <param name="objVer"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static void DownloadObjectSourceFiles(Vault vault, string path, ObjVerEx currentDocument)
        {
            // Sanity.
            if (null == vault)
                throw new ArgumentNullException(nameof(vault));
            if (null == currentDocument.ObjVer)
                throw new ArgumentNullException(nameof(currentDocument.ObjVer));

            // Get the files for the current ObjVer.
            var objectFiles = vault.ObjectFileOperations.GetFiles(currentDocument.ObjVer);

            var completePath = path;

            if (objectFiles.Count > 1)
            {
                completePath += @"\" + currentDocument.Title + @"\";
                Directory.CreateDirectory(completePath);
            }

            // Iterate over the files and download each in turn.
            foreach (ObjectFile objectFile in objectFiles)
            {

                // Where can we download it?
                var filePath = System.IO.Path.Combine(
                    completePath + objectFile.Title + "." + objectFile.Extension); // The name including extension.

                // Download the file.
                vault.ObjectFileOperations.DownloadFile(objectFile.ID, objectFile.Version, filePath);

            }

        }
    }
}
