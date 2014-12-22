using JCotton.DismSharp.Interop;

namespace JCotton.DismSharp {
    public class MountedWindowsImage {
        private string _imageFilePath;
        private uint _imageIndex;
        private bool _isMountedReadOnly;
        private string _mountPath;
        private ImageMountStatus _status;

        public string ImageFilePath => this._imageFilePath;
        public uint ImageIndex => this._imageIndex;
        public bool IsMountedReadOnly => this._isMountedReadOnly;
        public string MountPath => this._mountPath;
        public ImageMountStatus Status => this._status;

        public MountedWindowsImage(DismMountedImageInfo img) {
            this._mountPath = img.MountPath;
            this._imageFilePath = img.ImageFilePath;
            this._imageIndex = img.ImageIndex;
            this._isMountedReadOnly = img.MountMode == DismMountMode.DismReadOnly;
            this._status = img.MountStatus;
        }
    }
}
