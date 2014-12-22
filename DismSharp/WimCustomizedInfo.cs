using System;
using System.Runtime.InteropServices;
using JCotton.DismSharp.Interop;

namespace JCotton.DismSharp {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class WimCustomizedInfo {
        private uint _size;
        private uint _directoryCount;
        private uint _fileCount;
        private SYSTEMTIME _createdTime;
        private SYSTEMTIME _modifiedTime;

        public uint Size => this._size;
        public uint DirectoryCount => this._directoryCount;
        public uint FileCount => this._fileCount;
        public DateTime CreationTime => this._createdTime.ToDateTime();
        public DateTime LastModifiedTime => this._modifiedTime.ToDateTime();
    }
}
