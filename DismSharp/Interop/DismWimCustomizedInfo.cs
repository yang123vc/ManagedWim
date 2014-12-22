using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismWimCustomizedInfo {
        public uint Size;
        public uint DirectoryCount;
        public uint FileCount;
        public SYSTEMTIME CreatedTime;
        public SYSTEMTIME ModifiedTime;
    }
}
