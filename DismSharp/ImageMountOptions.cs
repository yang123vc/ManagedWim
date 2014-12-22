using System;

namespace JCotton.DismSharp {
    [Flags]
    public enum ImageMountOptions : uint {
        None = 0,
        ReadOnly = 1,
        Optimize = 2,
        CheckIntegrity = 4
    }
}
