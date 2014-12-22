using JCotton.WimSharp.Interop;

namespace JCotton.WimSharp {
    public enum WimCompressionType : uint {
        None = NativeMethods.WIM_COMPRESS_NONE,
        XPress = NativeMethods.WIM_COMPRESS_XPRESS,
        LZX = NativeMethods.WIM_COMPRESS_LZX,
        LZMS = NativeMethods.WIM_COMPRESS_LZMS
    }
}
