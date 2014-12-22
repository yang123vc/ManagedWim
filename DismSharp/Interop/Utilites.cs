using System;
using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    public static class Utilites {
        public static T[] PtrToArray<T>(IntPtr ptr, uint length) {
            IntPtr cur = ptr;
            T[] ret = new T[length];
            int size = Marshal.SizeOf<T>();
            for(uint i = 0; i < length; i++) {
                ret[i] = Marshal.PtrToStructure<T>(cur);
                cur += size;
            }
            return ret;
        }
    }
}
