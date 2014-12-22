using System;
using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismPackageInfo {
        public string PackageName;
        public PackageFeatureState PackageState;
        public PackageReleaseType ReleaseType;
        public SYSTEMTIME InstallTime;
        public bool Applicable;
        public string Copyright;
        public string Company;
        public SYSTEMTIME CreationTime;
        public string DisplayName;
        public string Description;
        public string InstallClient;
        public string InstallPackageName;
        public SYSTEMTIME LastUpdateTime;
        public string ProductName;
        public string ProductVersion;
        public RestartType RestartRequired;
        public FullyOfflineInstallable FullyOffline;
        public string SupportInformation;
        public IntPtr CustomProperty;
        public uint CustomPropertyCount;
        public IntPtr Feature;
        public uint FeatureCount;
    }
}
