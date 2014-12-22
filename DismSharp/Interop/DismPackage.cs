using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismPackage {
        public string PackageName;
        public PackageFeatureState PackageState;
        public PackageReleaseType ReleaseType;
        public SYSTEMTIME InstallTime;
    }
}
