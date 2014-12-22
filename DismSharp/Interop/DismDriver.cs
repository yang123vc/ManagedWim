using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismDriver {
        public string ManufacturerName;
        public string HardwareDescription;
        public string HardwareId;
        public WindowsImageArchitecture Architecture;
        public string ServiceName;
        public string CompatibleIds;
        public string ExcludeIds;
    }
}