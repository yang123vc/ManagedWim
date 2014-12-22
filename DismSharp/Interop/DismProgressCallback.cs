using System;

namespace JCotton.DismSharp.Interop {
    public delegate void DismProgressCallback(
        uint current,
        uint total,
        IntPtr userData
        );
}
