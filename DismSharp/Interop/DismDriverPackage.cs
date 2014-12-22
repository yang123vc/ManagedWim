using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismDriverPackage {
        public string PublishedName;
        public string OriginalFileName;
        public bool InBox;
        public string CatalogFile;
        public string ClassName;
        public string ClassGuid;
        public string ClassDescription;
        public bool BootCritical;
        public DriverSignature DriverSignature;
        public string ProviderName;
        public SYSTEMTIME Date;
        public uint MajorVersion;
        public uint MinorVersion;
        public uint Build;
        public uint Revision;
    }
}