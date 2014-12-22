using JCotton.DismSharp.Interop;

namespace JCotton.DismSharp {
    public class WindowsDriver {
        private string _manufacturerName;
        private string _hardwareDescription;
        private string _hardwareId;
        private WindowsImageArchitecture _architecture;
        private string _serviceName;
        private string _compatibleIds;
        private string _excludeIds;

        public string ManufacturerName => this._manufacturerName;
        public string HardwareDescription => this._hardwareDescription;
        public string HardwareId => this._hardwareId;
        public WindowsImageArchitecture Architecture => this._architecture;
        public string ServiceName => this._serviceName;
        public string CompatibleIds => this._compatibleIds;
        public string ExcludeIds => this._excludeIds;

        public WindowsDriver(DismDriver drv) {
            this._manufacturerName = drv.ManufacturerName;
            this._hardwareDescription = drv.HardwareDescription;
            this._hardwareId = drv.HardwareId;
            this._architecture = drv.Architecture;
            this._serviceName = drv.ServiceName;
            this._compatibleIds = drv.CompatibleIds;
            this._excludeIds = drv.ExcludeIds;
        }
    }
}
