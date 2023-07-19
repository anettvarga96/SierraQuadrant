using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MFiles.VAF.Configuration;
using MFiles.VAF.Configuration.JsonAdaptor;

namespace SierraQuadrant
{
    [DataContract]
    public class Configuration
    {
        #region Classes

        [MFClass(Required = true)]
        public MFIdentifier cDocumentArhivat = "cDocumentArhivat";

        #endregion

        #region Property Definitions

        [MFPropertyDef(Required = true)]
        public MFIdentifier pSocietate = "pSocietate";

        #endregion
    }
}