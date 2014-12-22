using System;
using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismFeatureInfo {
        string FeatureName;
        PackageFeatureState FeatureState;
        string DisplayName;
        string Description;
        RestartType RestartRequired;
        IntPtr CustomProperty;
        uint CustomPropertyCount;
    }
}