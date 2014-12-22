using System;
using JCotton.WimSharp.Interop;

namespace JCotton.WimSharp {
    [Flags]
    public enum WimMountOptions : uint {
        ReadOnly = NativeMethods.WIM_FLAG_MOUNT_READONLY,
        Verify = NativeMethods.WIM_FLAG_VERIFY,
        DisableReparsePointPathFixup = NativeMethods.WIM_FLAG_NO_RP_FIX,
        DisableDirectoryACLs = NativeMethods.WIM_FLAG_NO_DIRACL,
        DisableFileACLs = NativeMethods.WIM_FLAG_NO_FILEACL
    }
}
