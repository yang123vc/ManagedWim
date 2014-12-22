using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismMountedImageInfo {
        public string MountPath;
        public string ImageFilePath;
        public uint ImageIndex;
        public DismMountMode MountMode;
        public ImageMountStatus MountStatus;
    }
}