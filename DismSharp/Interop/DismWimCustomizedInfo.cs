using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismWimCustomizedInfo {
        uint Size;
        uint DirectoryCount;
        uint FileCount;
        SYSTEMTIME CreatedTime;
        SYSTEMTIME ModifiedTime;
    }
}