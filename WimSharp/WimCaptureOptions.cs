using System;
using JCotton.WimSharp.Interop;

namespace JCotton.WimSharp {
    [Flags]
    public enum WimCaptureOptions : uint {
        Verify = NativeMethods.WIM_FLAG_VERIFY,
        DisableReparsePointPathFixup = NativeMethods.WIM_FLAG_NO_RP_FIX,
        DisableDirectoryACLs = NativeMethods.WIM_FLAG_NO_DIRACL,
        DisableFileACLs = NativeMethods.WIM_FLAG_NO_FILEACL,
        WIMBoot = NativeMethods.WIM_FLAG_WIM_BOOT
    }
}
