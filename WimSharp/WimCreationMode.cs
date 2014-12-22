using JCotton.WimSharp.Interop;

namespace JCotton.WimSharp {
    public enum WimCreationMode : uint {
        CreateNew = NativeMethods.WIM_CREATE_NEW,
        CreateAlways = NativeMethods.WIM_CREATE_ALWAYS,
        OpenExisting = NativeMethods.WIM_OPEN_EXISTING,
        OpenAlways = NativeMethods.WIM_OPEN_ALWAYS
    }
}