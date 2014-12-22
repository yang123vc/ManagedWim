using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismMountedImageInfo {
        string MountPath;
        string ImageFilePath;
        uint ImageIndex;
        DismMountMode MountMode;
        ImageMountStatus MountStatus;
    }
}