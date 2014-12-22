using System;
using JCotton.DismSharp.Interop;

namespace JCotton.DismSharp {
    public class WimCustomizedInfo {
        private uint _size;
        private uint _directoryCount;
        private uint _fileCount;
        private DateTime _creationTime;
        private DateTime _lastModificationTime;

        public uint Size => this._size;
        public uint DirectoryCount => this._directoryCount;
        public uint FileCount => this._fileCount;
        public DateTime CreationTime => this._creationTime;
        public DateTime LastModificationTime => this._lastModificationTime;

        public WimCustomizedInfo(DismWimCustomizedInfo info) {
            this._size = info.Size;
            this._directoryCount = info.DirectoryCount;
            this._fileCount = info.FileCount;
            this._creationTime = info.CreatedTime.ToDateTime();
            this._lastModificationTime = info.ModifiedTime.ToDateTime();
        }
    }
}
