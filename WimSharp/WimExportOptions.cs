using System;
using JCotton.WimSharp.Interop;

namespace JCotton.WimSharp {
    [Flags]
    public enum WimExportOptions : uint {
        AllowDuplicates = NativeMethods.WIM_EXPORT_ALLOW_DUPLICATES,
        ExportOnlyResources = NativeMethods.WIM_EXPORT_ONLY_RESOURCES,
        ExportOnlyMetadata = NativeMethods.WIM_EXPORT_ONLY_METADATA,
        VerifySource = NativeMethods.WIM_EXPORT_VERIFY_SOURCE,
        VerifyDestination = NativeMethods.WIM_EXPORT_VERIFY_DESTINATION,
        WIMBoot = NativeMethods.WIM_FLAG_WIM_BOOT
    }
}
