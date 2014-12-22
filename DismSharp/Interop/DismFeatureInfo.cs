using System;
using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismFeatureInfo {
        public string FeatureName;
        public PackageFeatureState FeatureState;
        public string DisplayName;
        public string Description;
        public RestartType RestartRequired;
        public IntPtr CustomProperty;
        public uint CustomPropertyCount;
    }
}