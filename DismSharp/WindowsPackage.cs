using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JCotton.DismSharp.Interop;

namespace JCotton.DismSharp {
    public class WindowsPackage {
        private string _packageName;
        private PackageFeatureState _packageState;
        private PackageReleaseType _releaseType;
        private DateTime _installTime;
        private bool _applicable;
        private string _copyright;
        private string _company;
        private DateTime _creationTime;
        private string _displayName;
        private string _description;
        private string _installClient;
        private string _installPackageName;
        private DateTime _lastUpdatedTime;
        private string _productName;
        private string _productVersion;
        private RestartType _restartRequired;
        private FullyOfflineInstallable _fullyOffline;
        private string _supportInformation;
        private Dictionary<string, string> _customProperties;
        private Collection<WindowsFeature> _features;

        public string PackageName => this._packageName;
        public PackageReleaseType ReleaseType => this._releaseType;
        public PackageFeatureState PackageState => this._packageState;
        public DateTime InstallTime => this._installTime;
        public bool Applicable => this._applicable;
        public string Copyright => this._copyright;
        public string Company => this._company;
        public DateTime CreationTime => this._creationTime;
        public string DisplayName => this._displayName;
        public string Description => this._description;
        public string InstallClient => this._installClient;
        public string InstallPackageName => this._installPackageName;
        public DateTime LastUpdatedTime => this._lastUpdatedTime;
        public string ProductName => this._productName;
        public string ProductVersion => this._productVersion;
        public RestartType RestartRequired => this._restartRequired;
        public FullyOfflineInstallable FullyOffline => this._fullyOffline;
        public string SupportInformation => this._supportInformation;
        public IReadOnlyDictionary<string, string> CustomProperties => this._customProperties;
        public IReadOnlyCollection<WindowsFeature> Features => this._features;

        public WindowsPackage(DismPackage pkg) {
            this._packageName = pkg.PackageName;
            this._packageState = pkg.PackageState;
            this._releaseType = pkg.ReleaseType;
            this._installTime = pkg.InstallTime.ToDateTime();
        }

        public WindowsPackage(DismPackageInfo pkg) {
            this._packageName = pkg.PackageName;
            this._packageState = pkg.PackageState;
            this._releaseType = pkg.ReleaseType;
            this._installTime = pkg.InstallTime.ToDateTime();
            this._applicable = pkg.Applicable;
            this._copyright = pkg.Copyright;
            this._company = pkg.Company;
            this._creationTime = pkg.CreationTime.ToDateTime();
            this._displayName = pkg.DisplayName;
            this._description = pkg.Description;
            this._installClient = pkg.InstallClient;
            this._installPackageName = pkg.InstallPackageName;
            this._lastUpdatedTime = pkg.LastUpdateTime.ToDateTime();
            this._productName = pkg.ProductName;
            this._productVersion = pkg.ProductVersion;
            this._restartRequired = pkg.RestartRequired;
            this._fullyOffline = pkg.FullyOffline;
            this._supportInformation = pkg.SupportInformation;
            this._customProperties =
                Utilites.PtrToArray<DismCustomProperty>(
                    pkg.CustomProperty,
                    pkg.CustomPropertyCount
                    ).ToDictionary(
                        p => string.Format("{0}\\{1}", p.Path, p.Name),
                        p => p.Value
                    );
            this._features = new Collection<WindowsFeature>(
                Utilites.PtrToArray<DismFeature>(
                    pkg.Feature,
                    pkg.FeatureCount
                    )
                        .Select(f => new WindowsFeature(f))
                        .ToList()
                );
        }
    }
}
