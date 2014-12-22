using System;
using System.Runtime.InteropServices;
using System.Threading;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
#pragma warning disable 1591

namespace JCotton.WimSharp.Interop {
    public delegate uint WIMMessageCallback(
        uint dwMessageId,
        UIntPtr wParam,
        IntPtr lParam,
        IntPtr pvUserData
        );

    public delegate uint CopyProgressRoutine(
        long TotalFileSize,
        long TotalBytesTransferred,
        long StreamSize,
        long StreamBytesTransferred,
        uint dwStreamNumber,
        uint dwCallbackReason,
        IntPtr hSourceFile,
        IntPtr hDestinationFile,
        IntPtr lpData
        );

    public delegate uint WIMEnumImageFilesCallback(
        ref WIM_FIND_DATA pFindFileData,
        IntPtr pEnumFile,
        IntPtr pEnumContext
        );

    public delegate IntPtr FileIOCallbackOpenFile(
        [MarshalAs(UnmanagedType.LPWStr)]
        string pszFileName
        );

    public delegate bool FileIOCallbackCloseFile(
        IntPtr hFile
        );

    public delegate bool FileIOCallbackReadFile(
        IntPtr hFile,
        IntPtr pBuffer,
        uint nNumberOfBytesToRead,
        out uint pNumberOfBytesRead,
        ref NativeOverlapped pOverlapped
        );

    public delegate bool FileIOCallbackSetFilePointer(
        IntPtr hFile,
        long liDistanceToMove,
        out long pNewFilePointer,
        uint dwMoveMethod
        );

    public delegate bool FileIOCallbackGetFileSize(
        IntPtr hFile,
        out long pFileSize
        );

    public static class NativeMethods {
        #region Constants
        public const ulong WIM_GENERIC_READ = 0x80000000UL;
        public const ulong WIM_GENERIC_WRITE = 0x40000000UL;
        public const ulong WIM_GENERIC_MOUNT = 0x20000000UL;

        public const uint WIM_CREATE_NEW = 1;
        public const uint WIM_CREATE_ALWAYS = 2;
        public const uint WIM_OPEN_EXISTING = 3;
        public const uint WIM_OPEN_ALWAYS = 4;

        public const uint WIM_COMPRESS_NONE = 0;
        public const uint WIM_COMPRESS_XPRESS = 1;
        public const uint WIM_COMPRESS_LZX = 2;
        public const uint WIM_COMPRESS_LZMS = 3;

        public const uint WIM_CREATED_NEW = 0;
        public const uint WIM_OPENED_EXISTING = 1;

        public const uint WIM_FLAG_RESERVED = 0x00000001;
        public const uint WIM_FLAG_VERIFY = 0x00000002;
        public const uint WIM_FLAG_INDEX = 0x00000004;
        public const uint WIM_FLAG_NO_APPLY = 0x00000008;
        public const uint WIM_FLAG_NO_DIRACL = 0x00000010;
        public const uint WIM_FLAG_NO_FILEACL = 0x00000020;
        public const uint WIM_FLAG_SHARE_WRITE = 0x00000040;
        public const uint WIM_FLAG_FILEINFO = 0x00000080;
        public const uint WIM_FLAG_NO_RP_FIX = 0x00000100;
        public const uint WIM_FLAG_MOUNT_READONLY = 0x00000200;
        public const uint WIM_FLAG_MOUNT_FAST = 0x00000400;
        public const uint WIM_FLAG_MOUNT_LEGACY = 0x00000800;
        public const uint WIM_FLAG_APPLY_CI_EA = 0x00001000;
        public const uint WIM_FLAG_WIM_BOOT = 0x00002000;

        public const uint WIM_MOUNT_FLAG_MOUNTED = 0x00000001;
        public const uint WIM_MOUNT_FLAG_MOUNTING = 0x00000002;
        public const uint WIM_MOUNT_FLAG_REMOUNTABLE = 0x00000004;
        public const uint WIM_MOUNT_FLAG_INVALID = 0x00000008;
        public const uint WIM_MOUNT_FLAG_NO_WIM = 0x00000010;
        public const uint WIM_MOUNT_FLAG_NO_MOUNTDIR = 0x00000020;
        public const uint WIM_MOUNT_FLAG_MOUNTDIR_REPLACED = 0x00000040;
        public const uint WIM_MOUNT_FLAG_READWRITE = 0x00000100;

        public const uint WIM_COMMIT_FLAG_APPEND = 0x00000001;

        public const uint WIM_REFERENCE_APPEND = 0x00010000;
        public const uint WIM_REFERENCE_REPLACE = 0x00020000;

        public const uint WIM_EXPORT_ALLOW_DUPLICATES = 0x00000001;
        public const uint WIM_EXPORT_ONLY_RESOURCES = 0x00000002;
        public const uint WIM_EXPORT_ONLY_METADATA = 0x00000004;
        public const uint WIM_EXPORT_VERIFY_SOURCE = 0x00000008;
        public const uint WIM_EXPORT_VERIFY_DESTINATION = 0x00000010;

        public const uint INVALID_CALLBACK_VALUE = 0xFFFFFFFF;

        public const uint WIM_COPY_FILE_RETRY = 0x01000000;

        public const uint WIM_DELETE_MOUNTS_ALL = 0x00000001;

        public const uint WIM_LOGFILE_UTF8 = 0x00000001;

        public const uint WIM_MSG_SUCCESS = 0x00000000;
        public const uint WIM_MSG_DONE = 0xFFFFFFF0;
        public const uint WIM_MSG_SKIP_ERROR = 0xFFFFFFFE;
        public const uint WIM_MSG_ABORT_IMAGE = 0xFFFFFFFF;

        public const uint WIM_ATTRIBUTE_NORMAL = 0x00000000;
        public const uint WIM_ATTRIBUTE_RESOURCE_ONLY = 0x00000001;
        public const uint WIM_ATTRIBUTE_METADATA_ONLY = 0x00000002;
        public const uint WIM_ATTRIBUTE_VERIFY_DATA = 0x00000004;
        public const uint WIM_ATTRIBUTE_RP_FIX = 0x00000008;
        public const uint WIM_ATTRIBUTE_SPANNED = 0x00000010;
        public const uint WIM_ATTRIBUTE_READONLY = 0x00000020;
        #endregion Constants

        #region Methods
        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr WIMCreateFile(
            string pszWimPath,
            WimOpenMode dwDesiredAccess,
            WimCreationMode dwCreationDisposition,
            WimCreationOptions dwFlagsAndAttributes,
            WimCompressionType dwCompressionType,
            out uint pdwCreationResult
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMCloseHandle(
            IntPtr hObject
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMSetTemporaryPath(
            IntPtr hWim,
            string pszPath
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMSetReferenceFile(
            IntPtr hWim,
            string pszPath,
            uint dwFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMSplitFile(
            IntPtr hWim,
            string pszPartPath,
            ref long pliPartSize,
            uint dwFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMExportImage(
            IntPtr hImage,
            IntPtr hWim,
            WimExportOptions dwFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMDeleteImage(
            IntPtr hWim,
            uint dwImageIndex
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint WIMGetImageCount(
            IntPtr hWim
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMGetAttributes(
            IntPtr hWim,
            out WIM_INFO pWimInfo,
            uint cbWimInfo
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMSetBootImage(
            IntPtr hWim,
            uint dwImageIndex
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr WIMCaptureImage(
            IntPtr hWim,
            [MarshalAs(UnmanagedType.LPWStr)]
            string pszPath,
            WimCaptureOptions dwCaptureFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr WIMLoadImage(
            IntPtr hWim,
            uint dwImageIndex
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMApplyImage(
            IntPtr hImage,
            [MarshalAs(UnmanagedType.LPWStr)]
            string pszPath,
            WimApplyOptions dwApplyFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMGetImageInformation(
            IntPtr hImage,
            out IntPtr ppvImageInfo,
            out uint pcbImageInfo
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMSetImageInformation(
            IntPtr hImage,
            IntPtr pvImageInfo,
            uint cbImageInfo
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode)]
        public static extern uint WIMGetMessageCallbackCount(
            IntPtr hWim
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint WIMRegisterMessageCallback(
            IntPtr hWim,
            WIMMessageCallback fpMessageProc,
            IntPtr pvUserData
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMUnregisterMessageCallback(
            IntPtr hWim,
            WIMMessageCallback fpMessageProc
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMCopyFile(
            string pszExistingFileName,
            string pszNewFileName,
            CopyProgressRoutine pProgressRoutine,
            IntPtr pvData,
            ref bool pbCancel,
            uint dwCopyFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMMountImage(
            string pszMountPath,
            string pszWimFileName,
            uint dwImageIndex,
            string pszTempPath
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMMountImageHandle(
            IntPtr hImage,
            string pszMountPath,
            WimMountOptions dwMountFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMRemountImage(
            string pszMountPath,
            uint dwFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMCommitImageHandle(
            IntPtr hImage,
            WimCommitOptions dwCommitFlags,
            out IntPtr phNewImageHandle
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMUnmountImage(
            string pszMountPath,
            string pszWimFileName,
            uint dwImageIndex,
            bool bCommitChanges
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMUnmountImageHandle(
            IntPtr hImage,
            uint dwUnmountFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMGetMountedImages(
            IntPtr pMountList,
            ref uint pcbMountListLength
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMGetMountedImageInfo(
            MOUNTED_IMAGE_INFO_LEVELS fInfoLevelId,
            out uint pdwImageCount,
            IntPtr pMountInfo,
            uint cbMountInfoLength,
            out uint pcbReturnLength
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMGetMountedImageInfoFromHandle(
            IntPtr hImage,
            MOUNTED_IMAGE_INFO_LEVELS fInfoLevelId,
            IntPtr pMountInfo,
            uint cbMountInfoLength,
            out uint pcbReturnLength
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMGetMountedImageHandle(
            string pszMountPath,
            uint dwFlags,
            out IntPtr phWimHandle,
            out IntPtr phImageHandle
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMDeleteImageMounts(
            uint dwDeleteFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMRegisterLogFile(
            string pszLogFile,
            uint dwFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMUnregisterLogFile(
            string pszLogFile
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMExtractImagePath(
            IntPtr hImage,
            string pszImagePath,
            string pszDestinationPath,
            uint dwExtractFlags
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr WIMFindFirstImageFile(
            IntPtr hImage,
            string pwszFilePath,
            out WIM_FIND_DATA pFindFileData
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMFindNextImageFile(
            IntPtr hFindFile,
            out WIM_FIND_DATA pFindFileData
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMEnumImageFiles(
            IntPtr hImage,
            IntPtr pEnumFile,
            WIMEnumImageFilesCallback fpEnumImageCallback,
            IntPtr pEnumContext
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr WIMCreateImageFile(
            IntPtr hImage,
            string pwszFilePath,
            uint dwDesiredAccess,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMReadImageFile(
            IntPtr hImgFile,
            IntPtr pbBuffer,
            uint dwBytesToRead,
            out uint pdwBytesRead,
            ref NativeOverlapped lpOverlapped
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMInitFileIOCallbacks(
            IntPtr pCallbacks
            );

        [DllImport("wimgapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WIMSetFileIOCallbackTemporaryPath(
            string pszPath
            );

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree(
            IntPtr hMem
            );
        #endregion Methods
    }

    public enum WIM_MESSAGE_TYPE {
        WIM_MSG = 0x9476,
        WIM_MSG_TEXT,
        WIM_MSG_PROGRESS,
        WIM_MSG_PROCESS,
        WIM_MSG_SCANNING,
        WIM_MSG_SETRANGE,
        WIM_MSG_SETPOS,
        WIM_MSG_STEPIT,
        WIM_MSG_COMPRESS,
        WIM_MSG_ERROR,
        WIM_MSG_ALIGNMENT,
        WIM_MSG_RETRY,
        WIM_MSG_SPLIT,
        WIM_MSG_FILEINFO,
        WIM_MSG_INFO,
        WIM_MSG_WARNING,
        WIM_MSG_CHK_PROCESS,
        WIM_MSG_WARNING_OBJECTID,
        WIM_MSG_STALE_MOUNT_DIR,
        WIM_MSG_STALE_MOUNT_FILE,
        WIM_MSG_MOUNT_CLEANUP_PROGRESS,
        WIM_MSG_CLEANUP_SCANNING_DRIVE,
        WIM_MSG_IMAGE_ALREADY_MOUNTED,
        WIM_MSG_CLEANUP_UNMOUNTING_IMAGE,
        WIM_MSG_QUERY_ABORT,
        WIM_MSG_IO_RANGE_START_REQUEST_LOOP,
        WIM_MSG_IO_RANGE_END_REQUEST_LOOP,
        WIM_MSG_IO_RANGE_REQUEST,
        WIM_MSG_IO_RANGE_RELEASE,
        WIM_MSG_VERIFY_PROGRESS,
        WIM_MSG_COPY_BUFFER,
        WIM_MSG_METADATA_EXCLUDE,
        WIM_MSG_GET_APPLY_ROOT,
        WIM_MSG_MDPAD,
        WIM_MSG_STEPNAME,
        WIM_MSG_PERFILE_COMPRESS,
        WIM_MSG_CHECK_CI_EA_PREREQUISITE_NOT_MET,
        WIM_MSG_JOURNALING_ENABLED
    }

    public enum MOUNTED_IMAGE_INFO_LEVELS {
        MountedImageInfoLevel0,
        MountedImageInfoLevel1,
        MountedImageInfoLevelInvalid
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WIM_INFO {
        [MarshalAs(UnmanagedType.LPWStr, SizeConst = 260)]
        public string WimPath;
        public Guid Guid;
        public uint ImageCount;
        public WimCompressionType CompressionType;
        public ushort PartNumber;
        public ushort TotalParts;
        public uint BootIndex;
        public WimFileAttributes WimAttributes;
        public WimCreationOptions WimFlagsAndAttr;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WIM_MOUNT_LIST {
        [MarshalAs(UnmanagedType.LPWStr, SizeConst = 260)]
        public string WimPath;
        [MarshalAs(UnmanagedType.LPWStr, SizeConst = 260)]
        public string MountPath;
        public uint ImageIndex;
        public bool MountedForRW;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WIM_MOUNT_INFO_LEVEL1 {
        [MarshalAs(UnmanagedType.LPWStr, SizeConst = 260)]
        public string WimPath;
        [MarshalAs(UnmanagedType.LPWStr, SizeConst = 260)]
        public string MountPath;
        public uint ImageIndex;
        public uint MountFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WIM_IO_RANGE_CALLBACK {
        public IntPtr pSession;
        public long Offset;
        public long Size;
        public bool Available;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WIM_FIND_DATA {
        public uint dwFileAttributes;
        public FILETIME ftCreationTime;
        public FILETIME ftLastAccessTime;
        public FILETIME ftLastWriteTime;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public uint dwReserved0;
        public uint dwReserved1;
        [MarshalAs(UnmanagedType.LPWStr, SizeConst = 260)]
        public string cFileName;
        [MarshalAs(UnmanagedType.LPWStr, SizeConst = 14)]
        public string cAlternateFileName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] bHash;
        public IntPtr pSecurityDescriptor;
        public IntPtr ppszAlternateStreamNames;
        public IntPtr pbReparseData;
        public uint cbReparseData;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SFileIOCallbackInfo {
        public FileIOCallbackOpenFile pfnOpenFile;
        public FileIOCallbackCloseFile pfnCloseFile;
        public FileIOCallbackReadFile pfnReadFile;
        public FileIOCallbackSetFilePointer pfnSetFilePointer;
        public FileIOCallbackGetFileSize pfnGetFileSize;
    }
}
