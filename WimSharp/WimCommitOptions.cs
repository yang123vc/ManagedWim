using JCotton.WimSharp.Interop;

namespace JCotton.WimSharp {
    public enum WimCommitOptions : uint {
        Append = NativeMethods.WIM_COMMIT_FLAG_APPEND,
        Verify = NativeMethods.WIM_FLAG_VERIFY,
        DisableReparsePointPathFixup = NativeMethods.WIM_FLAG_NO_RP_FIX,
        DisableDirectoryACLs = NativeMethods.WIM_FLAG_NO_DIRACL,
        DisableFileACLs = NativeMethods.WIM_FLAG_NO_FILEACL
    }
}
