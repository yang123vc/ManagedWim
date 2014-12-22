using System.Runtime.InteropServices;
using JCotton.DismSharp.Interop;

namespace JCotton.DismSharp {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class MountedImageInfo {
        private string _mountPath;
        private string _imageFilePath;
        private uint _imageIndex;
        private DismMountMode _mountMode;
        private ImageMountStatus _mountStatus;

        public string MountPath => this._mountPath;
        public string ImageFilePath => this._imageFilePath;
        public uint Index => this._imageIndex;
        public bool IsMountedReadOnly => this._mountMode == DismMountMode.DismReadOnly;
        public ImageMountStatus MountStatus => this._mountStatus;
    }
}
