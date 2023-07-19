using System;
using System.Diagnostics;
using System.IO;
using MFiles.VAF;
using MFiles.VAF.AppTasks;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Core;
using MFilesAPI;
using SierraQuadrant.utils;

namespace SierraQuadrant
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public class VaultApplication
        : ConfigurableVaultApplicationBase<Configuration>
    {
        [StateAction("sWfDescarcareDocumenteDescarcare")]
        public void DownloadDocuments(StateEnvironment env)
        {
            var myObject = env.ObjVerEx;

            var societateID = myObject.GetLookupID(Configuration.pSocietate);

            var searchBuilder = new MFSearchBuilder(myObject.Vault);
            searchBuilder.ObjType((int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument);
            searchBuilder.Class(Configuration.cDocumentArhivat);
            searchBuilder.Property(Configuration.pSocietate, MFDataType.MFDatatypeLookup, societateID);
            searchBuilder.Deleted(false);
            
            var searchResults = searchBuilder.FindEx();

            if (searchResults.Count > 0)
            {
                var path = @"C:\PROIECTE\ARHIVA [SIERRA QUADRANT]\DOCUMENTE DESCARCATE\" + myObject.Title.Replace(':', '.') + @"\";

                Directory.CreateDirectory(path);

                foreach (var document in searchResults)
                {
                    MFilesUtils.DownloadObjectSourceFiles(myObject.Vault, path, document);
                }
            }

        }

    }
}