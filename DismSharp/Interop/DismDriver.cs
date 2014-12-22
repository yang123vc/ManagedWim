using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismDriver {
        string ManufacturerName;
        string HardwareDescription;
        string HardwareId;
        WindowsImageArchitecture Architecture;
        string ServiceName;
        string CompatibleIds;
        string ExcludeIds;
    }
}