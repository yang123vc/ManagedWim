using System;
using JCotton.WimSharp.Interop;

namespace JCotton.WimSharp {
    [Flags]
    public enum WimFileAttributes : uint {
        Normal = NativeMethods.WIM_ATTRIBUTE_NORMAL,
        OnlyResources = NativeMethods.WIM_ATTRIBUTE_RESOURCE_ONLY,
        OnlyMetadata = NativeMethods.WIM_ATTRIBUTE_METADATA_ONLY,
        CanBeVerified = NativeMethods.WIM_ATTRIBUTE_VERIFY_DATA,
        ReparsePointPathsFixed = NativeMethods.WIM_ATTRIBUTE_RP_FIX,
        Spanned = NativeMethods.WIM_ATTRIBUTE_SPANNED,
        Readonly = NativeMethods.WIM_ATTRIBUTE_READONLY
    }
}
