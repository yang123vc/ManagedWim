using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using JCotton.DismSharp.Interop;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace JCotton.DismSharp {
    public class WindowsImage : IDisposable {
        private ImageType _imageType;
        private uint _imageIndex;
        private string _imageName;
        private string _imageDescription;
        private ulong _imageSize;
        private WindowsImageArchitecture _architecture;
        private string _productName;
        private string _editionId;
        private string _installationType;
        private string _hal;
        private string _productType;
        private string _productSuite;
        private Version _version;
        private uint _spBuild;
        private uint _spLevel;
        private ImageBootable _bootable;
        private string _systemRoot;
        private Collection<string> _languages;
        private uint _defaultLanguageIndex;
        private WimCustomizedInfo _customizedInfo;

        private uint _session = 0;
        private string _mountPath;
        private string _imagePath;

        public ImageType ImageType => this._imageType;
        public uint ImageIndex => this._imageIndex;
        public string ImageName => this._imageName;
        public string ImageDescription => this._imageDescription;
        public ulong ImageSize => this._imageSize;
        public WindowsImageArchitecture Architecture => this._architecture;
        public string ProductName => this._productName;
        public string EditionId => this._editionId;
        public string InstallationType => this._installationType;
        public string Hal => this._hal;
        public string ProductType => this._productType;
        public string ProductSuite => this._productSuite;
        public Version Version => this._version;
        public uint SpBuild => this._spBuild;
        public uint SpLevel => this._spLevel;
        public ImageBootable Bootable => this._bootable;
        public string SystemRoot => this._systemRoot;
        public IReadOnlyList<string> Languages => this._languages;
        public string DefaultLanguage => this._languages[(int)this._defaultLanguageIndex];
        public WimCustomizedInfo CustomizedInfo => this._customizedInfo;
        public string MountPath => this._mountPath;

        public WindowsImage(DismImageInfo info) {
            this._imageType = info.ImageType;
            this._imageIndex = info.ImageIndex;
            this._imageName = info.ImageName;
            this._imageDescription = info.ImageDescription;
            this._imageSize = info.ImageSize;
            this._architecture = info.Architecture;
            this._productName = info.ProductName;
            this._editionId = info.EditionId;
            this._installationType = info.InstallationType;
            this._hal = info.Hal;
            this._productType = info.ProductType;
            this._productSuite = info.ProductSuite;
            this._version = new Version(
                (int)info.MajorVersion,
                (int)info.MinorVersion,
                (int)info.Build
                );
            this._spBuild = info.SpBuild;
            this._spLevel = info.SpLevel;
            this._bootable = info.Bootable;
            this._systemRoot = info.SystemRoot;
            this._languages = new Collection<string>(
                Utilites.PtrToArray<DismString>(
                    info.Language,
                    info.LanguageCount
                    )
                        .Select(l => l.Value)
                        .ToList()
                );
            this._defaultLanguageIndex = info.DefaultLanguageIndex;
            this._customizedInfo = info.CustomizedInfo == IntPtr.Zero
                ? null
                : new WimCustomizedInfo(
                    Marshal.PtrToStructure<DismWimCustomizedInfo>(info.CustomizedInfo)
                    );
        }

        ~WindowsImage() {
            this.Dispose(false);
        }

        public DismSession OpenSession() {
            uint session;
            NativeMethods.DismOpenSession(
                this._mountPath,
                null,
                null,
                out session
                );
            return new DismSession(session, "Windows", "");
        }

        public void Mount(
            [NotNull] string path,
            ImageMountOptions options
            ) => this.Mount(path, options, null);

        public void Mount(
            [NotNull] string path,
            ImageMountOptions options,
            [CanBeNull] IProgress<DismEventArgs> progress
            ) => Task.Run<Task>(async () => await this.MountAsync(path, options)).Wait();

        public async Task MountAsync(
            [NotNull] string path,
            ImageMountOptions options
            ) => await this.MountAsync(
                path,
                options,
                CancellationToken.None,
                null);

        public async Task MountAsync(
            [NotNull] string path,
            ImageMountOptions options,
            CancellationToken cancellationToken,
            [CanBeNull] IProgress<DismEventArgs> progress
            ) {
            if(path == null)
                throw new ArgumentNullException("path");
            if(File.Exists(path))
                throw new ArgumentException("Path is a file", "path");
            if(Directory.Exists(path) && Directory.GetFileSystemEntries(path).Length != 0)
                throw new ArgumentException("Path refers to a non-empty directory", "path");
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
            await Task.Run(() =>
                NativeMethods.DismMountImage(
                    this._imagePath,
                    path,
                    this._imageIndex,
                    null,
                    DismImageIdentifier.DismImageIndex,
                    options,
                    cancellationToken.WaitHandle.SafeWaitHandle,
                    (current, total, userData) => progress?.Report(new DismEventArgs(current, total)),
                    IntPtr.Zero
                    ));
        }

        public void Unmount(
            ImageCommitOptions options,
            bool commit
            ) => Task.Run<Task>(async () => await this.UnmountAsync(options, commit)).Wait();

        public void Unmount(
            ImageCommitOptions options,
            bool commit,
            [CanBeNull] IProgress<DismEventArgs> progress
            ) => Task.Run<Task>(async () => await this.UnmountAsync(options, commit, progress)).Wait();

        public async Task UnmountAsync(
            ImageCommitOptions options,
            bool commit
            ) => await this.UnmountAsync(options, commit, CancellationToken.None, null);

        public async Task UnmountAsync(
            ImageCommitOptions options,
            bool commit,
            CancellationToken cancellationToken
            ) => await this.UnmountAsync(options, commit, cancellationToken, null);

        public async Task UnmountAsync(
            ImageCommitOptions options,
            bool commit,
            [CanBeNull] IProgress<DismEventArgs> progress
            ) => await this.UnmountAsync(options, commit, CancellationToken.None, progress);

        public async Task UnmountAsync(
            ImageCommitOptions options,
            bool commit,
            CancellationToken cancellationToken,
            [CanBeNull] IProgress<DismEventArgs> progress
            ) {
            DismCommitAndUnmountFlags flags = 0;
            if((options & ImageCommitOptions.Append) != 0)
                flags |= DismCommitAndUnmountFlags.Append;
            if((options & ImageCommitOptions.GenerateIntegrityData) != 0)
                flags |= DismCommitAndUnmountFlags.GenerateIntegrity;
            if(commit)
                flags |= DismCommitAndUnmountFlags.Commit;
            else
                flags |= DismCommitAndUnmountFlags.Discard;
            await Task.Run(() =>
                NativeMethods.DismUnmountImage(
                    this._mountPath,
                    flags,
                    cancellationToken.WaitHandle.SafeWaitHandle,
                    (current, total, userData) => progress?.Report(new DismEventArgs(current, total)),
                    IntPtr.Zero
                    )
                );
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            throw new NotImplementedException();
        }
    }
}
