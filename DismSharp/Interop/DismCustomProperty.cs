using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismCustomProperty {
        public string Name;
        public string Value;
        public string Path; 
    }
}
