using System;
using JCotton.WimSharp.Interop;

namespace JCotton.WimSharp {
    [Flags]
    public enum WimOpenMode : ulong {
        Read = NativeMethods.WIM_GENERIC_READ,
        Write = NativeMethods.WIM_GENERIC_WRITE,
        Mount = NativeMethods.WIM_GENERIC_MOUNT
    }
}
