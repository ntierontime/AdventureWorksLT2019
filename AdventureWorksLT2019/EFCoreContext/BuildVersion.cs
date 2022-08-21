using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace AdventureWorksLT2019.EFCoreContext
{
    public partial class BuildVersion
    {
        public BuildVersion()
        {

        }
        public byte SystemInformationID { get; set; }

        public string Database_Version { get; set; } = null!;

        public System.DateTime VersionDate { get; set; }

        public System.DateTime ModifiedDate { get; set; }

    }
}

