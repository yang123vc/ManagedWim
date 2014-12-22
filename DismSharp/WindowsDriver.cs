using System.Runtime.InteropServices;

namespace JCotton.DismSharp {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
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
    }
}
