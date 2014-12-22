using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismDriverPackage {
        string PublishedName;
        string OriginalFileName;
        bool InBox;
        string CatalogFile;
        string ClassName;
        string ClassGuid;
        string ClassDescription;
        bool BootCritical;
        DriverSignature DriverSignature;
        string ProviderName;
        SYSTEMTIME Date;
        uint MajorVersion;
        uint MinorVersion;
        uint Build;
        uint Revision;
    }
}