using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JCotton.DismSharp.Interop;
using JetBrains.Annotations;

namespace JCotton.DismSharp {
    public class DismSession : IDisposable {
        private uint _session;
        private bool _isOpen;
        private string _windowsDirectory;
        private string _systemDrive;

        public string WindowsDirectory => this._windowsDirectory;
        public string SystemDrive => this._systemDrive;
        public bool IsOpen => this._isOpen;

        public DismSession(uint session, string windowsDirectory, string systemDrive) {
            this._session = session;
            this._windowsDirectory = windowsDirectory;
            this._systemDrive = systemDrive;
            this._isOpen = true;
        }

        public void AddDriver([NotNull] string infPath, bool forceUnsigned) {
            if(infPath == null)
                throw new ArgumentNullException("infPath");
            if(!File.Exists(infPath))
                throw new ArgumentException("Path does not exist", "infPath");
            if(!Path.GetExtension(infPath).Equals(".inf", StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException("Path does not refer to an inf", "infPath");
            NativeMethods.DismAddDriver(this._session, infPath, forceUnsigned);
        }

        public Task AddPackageAsync(
            [NotNull] string packagePath,
            bool ignoreApplicabilityChecks,
            bool preventPending
            ) => this.AddPackageAsync(packagePath, ignoreApplicabilityChecks, preventPending, CancellationToken.None, null);

        public Task AddPackageAsync(
            [NotNull] string packagePath,
            bool ignoreApplicabilityChecks,
            bool preventPending,
            CancellationToken cancellationToken
            ) => this.AddPackageAsync(packagePath, ignoreApplicabilityChecks, preventPending, cancellationToken, null);

        public Task AddPackageAsync(
            [NotNull] string packagePath,
            bool ignoreApplicabilityChecks,
            bool preventPending,
            [CanBeNull] IProgress<DismEventArgs> progress
            ) => this.AddPackageAsync(packagePath, ignoreApplicabilityChecks, preventPending, CancellationToken.None, progress);

        public async Task AddPackageAsync(
            [NotNull] string packagePath,
            bool ignoreApplicabilityChecks,
            bool preventPending,
            CancellationToken cancellationToken,
            [CanBeNull] IProgress<DismEventArgs> progress
            ) {
            if(packagePath == null)
                throw new ArgumentNullException("packagePath");
            if(!Directory.Exists(packagePath)) {
                if(!File.Exists(packagePath))
                    throw new ArgumentException("Path does not exist", "packagePath");
                string ext = Path.GetExtension(packagePath);
                if(!ext.Equals(".cab", StringComparison.InvariantCultureIgnoreCase) &&
                    !ext.Equals(".msu", StringComparison.InvariantCultureIgnoreCase))
                    throw new ArgumentException(
                        "Path must refer to a directory of an expanded package or a cab or an msu.", "packagePath");
            }
            await Task.Run(() =>
                NativeMethods.DismAddPackage(
                    this._session,
                    packagePath,
                    ignoreApplicabilityChecks,
                    preventPending,
                    cancellationToken.WaitHandle.SafeWaitHandle,
                    (current, total, userData) => progress?.Report(new DismEventArgs(current, total)),
                    IntPtr.Zero
                    )
                );
        }

        public Task CommitChangesAsync(
            ImageCommitOptions options
            ) => this.CommitChangesAsync(options, CancellationToken.None, null);

        public Task CommitChangesAsync(
            ImageCommitOptions options,
            CancellationToken cancellationToken
            ) => this.CommitChangesAsync(options, cancellationToken, null);

        public Task CommitChangesAsync(
            ImageCommitOptions options,
            [CanBeNull] IProgress<DismEventArgs> progress
            ) => this.CommitChangesAsync(options, CancellationToken.None, progress);

        public async Task CommitChangesAsync(
            ImageCommitOptions options,
            CancellationToken cancellationToken,
            [CanBeNull] IProgress<DismEventArgs> progress
            ) {
            DismCommitAndUnmountFlags flags = 0;
            if((options & ImageCommitOptions.GenerateIntegrityData) != 0)
                flags |= DismCommitAndUnmountFlags.GenerateIntegrity;
            if((options & ImageCommitOptions.Append) != 0)
                flags |= DismCommitAndUnmountFlags.Append;
            await Task.Run(() =>
                NativeMethods.DismCommitImage(
                    this._session,
                    flags,
                    cancellationToken.WaitHandle.SafeWaitHandle,
                    (current, total, userData) => progress?.Report(new DismEventArgs(current, total)),
                    IntPtr.Zero
                    )
                );
        }

        public void Close() {
            NativeMethods.DismCloseSession(this._session);
            this._isOpen = false;
        }

        public void Dispose() {
            this.Dispose(true);
        }

        private void Dispose(bool disposing) {
            this.Close();
        }
    }
}
