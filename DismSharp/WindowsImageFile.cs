using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using JCotton.DismSharp.Interop;
using JetBrains.Annotations;

namespace JCotton.DismSharp {
    public class WindowsImageFile : IDisposable {
        private Collection<WindowsImage> _images;
        private string _path;

        public IReadOnlyList<WindowsImage> Images => this._images;
        public string Path => this._path;

        public WindowsImageFile([NotNull] string path) {
            if(path == null) throw new ArgumentNullException("path");
            if(!File.Exists(path)) throw new ArgumentException("Path does not exist", "path");
            this._path = path;
            IntPtr imageInfoPtr;
            uint infoCount;
            NativeMethods.DismGetImageInfo(path, out imageInfoPtr, out infoCount);
            this._images = new Collection<WindowsImage>(
                Utilites.PtrToArray<DismImageInfo>(
                    imageInfoPtr,
                    infoCount
                    )
                    .Select(info => new WindowsImage(info))
                    .ToList()
                );
            NativeMethods.DismDelete(imageInfoPtr);
        }

        ~WindowsImageFile() {
            this.Dispose(false);
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            foreach(var image in this._images) {
                image.Dispose();
            }
        }
    }
}
