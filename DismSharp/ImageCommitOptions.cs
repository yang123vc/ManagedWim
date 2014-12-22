using System;

namespace JCotton.DismSharp {
    [Flags]
    public enum ImageCommitOptions {
        GenerateIntegrityData = 0x10000,
        Append = 0x20000
    }
}
