using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismPackage {
        private string PackageName;
        private PackageFeatureState PackageState;
        private PackageReleaseType ReleaseType;
        private SYSTEMTIME InstallTime;
    }
}
