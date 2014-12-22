using System;
using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismPackageInfo {
        string PackageName;
        PackageFeatureState PackageState;
        PackageReleaseType ReleaseType;
        SYSTEMTIME InstallTime;
        bool Applicable;
        string Copyright;
        string Company;
        SYSTEMTIME CreationTime;
        string DisplayName;
        string Description;
        string InstallClient;
        string InstallPackageName;
        SYSTEMTIME LastUpdateTime;
        string ProductName;
        string ProductVersion;
        RestartType RestartRequired;
        FullyOfflineInstallable FullyOffline;
        string SupportInformation;
        IntPtr CustomProperty;
        uint CustomPropertyCount;
        IntPtr Feature;
        uint FeatureCount;
    }
}
