using System;
using JCotton.DismSharp.Interop;

namespace JCotton.DismSharp {
    public class WindowsDriverPackage {
        private string _publishedName;
        private string _originalFileName;
        private bool _isInnBox;
        private string _catalogFile;
        private string _className;
        private Guid _classGuid;
        private string _classDescription;
        private bool _isBootCritical;
        private DriverSignature _driverSignature;
        private string _providerName;
        private DateTime _date;
        private Version _version;

        public string PublishedName => this._publishedName;
        public string OriginalFileName => this._originalFileName;
        public bool IsInnBox => this._isInnBox;
        public string CatalogFile => this._catalogFile;
        public string ClassName => this._className;
        public Guid ClassGuid => this._classGuid;
        public string ClassDescription => this._classDescription;
        public bool IsBootCritical => this._isBootCritical;
        public DriverSignature DriverSignature => this._driverSignature;
        public string ProviderName => this._providerName;
        public DateTime Date => this._date;
        public Version Version => this._version;

        public WindowsDriverPackage(DismDriverPackage pkg) {
            this._publishedName = pkg.PublishedName;
            this._originalFileName = pkg.OriginalFileName;
            this._isInnBox = pkg.InBox;
            this._catalogFile = pkg.CatalogFile;
            this._className = pkg.ClassName;
            this._classGuid = Guid.Parse(pkg.ClassGuid);
            this._classDescription = pkg.ClassDescription;
            this._isBootCritical = pkg.BootCritical;
            this._driverSignature = pkg.DriverSignature;
            this._providerName = pkg.ProviderName;
            this._date = pkg.Date.ToDateTime();
            this._version = new Version(
                (int)pkg.MajorVersion,
                (int)pkg.MinorVersion,
                (int)pkg.Build,
                (int)pkg.Revision
                );
        }
    }
}
