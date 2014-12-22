using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace JCotton.DismSharp.Interop {
    public static class NativeMethods {
        #region Constants
        public const string DISM_ONLINE_STRING = "DISM_{53BFAE52-B167-4E2F-A258-0A37B57FF845}";
        public const int DISM_SESSION_DEFAULT = 0;

        public const uint DISM_COMMIT_IMAGE = 0x00000000;
        public const uint DISM_DISCARD_IMAGE = 0x00000001;

        public const uint DISM_COMMIT_MASK = 0xffff0000;

        public const int ERROR_SUCCESS_REBOOT_REQUIRED = 3010;
        public const int DISMAPI_S_RELOAD_IMAGE_SESSION_REQUIRED = 1;
        public const int DISMAPI_E_DISMAPI_NOT_INITIALIZED = -1073479679;
        public const int DISMAPI_E_SHUTDOWN_IN_PROGRESS = -1073479678;
        public const int DISMAPI_E_OPEN_SESSION_HANDLES = -1073479677;
        public const int DISMAPI_E_INVALID_DISM_SESSION = -1073479676;
        public const int DISMAPI_E_INVALID_IMAGE_INDEX = -1073479675;
        public const int DISMAPI_E_INVALID_IMAGE_NAME = -1073479674;
        public const int DISMAPI_E_UNABLE_TO_UNMOUNT_IMAGE_PATH = -1073479673;
        public const int DISMAPI_E_LOGGING_DISABLED = -1073479671;
        public const int DISMAPI_E_OPEN_HANDLES_UNABLE_TO_UNMOUNT_IMAGE_PATH = -1073479670;
        public const int DISMAPI_E_OPEN_HANDLES_UNABLE_TO_MOUNT_IMAGE_PATH = -1073479669;
        public const int DISMAPI_E_OPEN_HANDLES_UNABLE_TO_REMOUNT_IMAGE_PATH = -1073479668;
        public const int DISMAPI_E_PARENT_FEATURE_DISABLED = -1073479667;
        public const int DISMAPI_E_MUST_SPECIFY_ONLINE_IMAGE = -1073479666;
        public const int DISMAPI_E_INVALID_PRODUCT_KEY = -1073479665;
        public const int DISMAPI_E_NEEDS_REMOUNT = -1051655916;
        public const int DISMAPI_E_UNKNOWN_FEATURE = -2146498548;
        public const int DISMAPI_E_BUSY = -2146498302;
        #endregion Constants

        #region Methods
        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismInitialize(
            DismLogLevel logLevel,
            [CanBeNull] string logFilePath,
            [CanBeNull] string scratchDirectory
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismShutdown();

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismMountImage(
            [NotNull] string imageFilePath,
            [NotNull] string mountPath,
            uint imageIndex,
            [CanBeNull] string imageName,
            DismImageIdentifier imageIdentifier,
            ImageMountOptions flags,
            [CanBeNull] SafeWaitHandle cancelEvent,
            [CanBeNull] DismProgressCallback progress,
            IntPtr userData
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismUnmountImage(
            [NotNull] string mountPath,
            DismCommitAndUnmountFlags flags,
            [CanBeNull] SafeWaitHandle cancelEvent,
            [CanBeNull] DismProgressCallback progress,
            IntPtr userData
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismOpenSession(
            [NotNull] string imagePath,
            [CanBeNull] string windowsDirectory,
            [CanBeNull] string systemDrive,
            out uint dismSession
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismCloseSession(
            uint dismSession
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismGetLastErrorMessage(
            out IntPtr errorMessage
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismRemountImage(
            [NotNull] string mountPath
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismCommitImage(
            uint session,
            DismCommitAndUnmountFlags flags,
            [CanBeNull] SafeWaitHandle cancelEvent,
            [CanBeNull] DismProgressCallback progress,
            IntPtr userData
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismGetImageInfo(
            [NotNull] string imageFilePath,
            out IntPtr imageInfo,
            out uint count
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismGetMountedImageInfo(
            out IntPtr imageInfo,
            out uint count
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismCleanupMountpoints();

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismCheckImageHealth(
            uint session,
            bool scanImage,
            [CanBeNull] SafeWaitHandle cancelEvent,
            [CanBeNull] DismProgressCallback progress,
            IntPtr userData,
            out ImageHealthState imageHealth
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismRestoreImageHealth(
            uint session,
            [CanBeNull, MarshalAs(UnmanagedType.LPArray)] string[] sourcePaths,
            uint sourcePathCount,
            bool limitAccess,
            [CanBeNull] SafeWaitHandle cancelEvent,
            [CanBeNull] DismProgressCallback progress,
            IntPtr userData
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismDelete(
            IntPtr dismStructure
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismAddPackage(
            uint session,
            [NotNull] string packagePath,
            bool ignoreCheck,
            bool preventPending,
            [CanBeNull] SafeWaitHandle cancelEvent,
            [CanBeNull] DismProgressCallback progress,
            IntPtr userData
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismRemovePackage(
            uint session,
            [NotNull] string identifier,
            PackageIdentifier packageIdentifier,
            [CanBeNull] SafeWaitHandle cancelEvent,
            [CanBeNull] DismProgressCallback progress,
            IntPtr userData
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismEnableFeature(
            uint session,
            [NotNull] string featureName,
            [CanBeNull] string identifier,
            PackageIdentifier packageIdentifier,
            bool limitAccess,
            [CanBeNull, MarshalAs(UnmanagedType.LPArray)] string[] sourcePaths,
            uint sourcePathCount,
            bool enableAll,
            [CanBeNull] SafeWaitHandle cancelEvent,
            [CanBeNull] DismProgressCallback progress,
            IntPtr userData
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismDisableFeature(
            uint session,
            [NotNull] string featureName,
            [CanBeNull] string packageName,
            bool removePayload,
            [CanBeNull] SafeWaitHandle cancelEvent,
            [CanBeNull] DismProgressCallback progress,
            IntPtr userData
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismGetPackages(
            uint session,
            out IntPtr package,
            out uint count
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismGetPackageInfo(
            uint session,
            [NotNull] string identifier,
            PackageIdentifier packageIdentifier,
            out IntPtr packageInfo
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismGetFeatures(
            uint session,
            [CanBeNull] string identifier,
            PackageIdentifier packageIdentifier,
            out IntPtr feature,
            out uint featureCount
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismGetFeatureInfo(
            uint session,
            [NotNull] string featureName,
            [CanBeNull] string identifier,
            PackageIdentifier packageIdentifier,
            out IntPtr featureInfo
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismGetFeatureParent(
            uint session,
            [NotNull] string featureName,
            [CanBeNull] string identifier,
            PackageIdentifier packageIdentifier,
            out IntPtr feature,
            out uint count
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismApplyUnattend(
            uint session,
            [NotNull] string unattendFile,
            bool singleSession
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismAddDriver(
            uint session,
            [NotNull] string driverPath,
            bool forceUnsigned
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismRemoveDriver(
            uint session,
            [NotNull] string driverPath
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismGetDrivers(
            uint session,
            bool allDrivers,
            out IntPtr driverPackage,
            out uint count
            );

        [DllImport("dismapi.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int DismGetDriverInfo(
            uint session,
            [NotNull] string driverPath,
            out IntPtr driver,
            out uint count,
            out IntPtr driverPackage
            );
        #endregion Methods
    }
}
