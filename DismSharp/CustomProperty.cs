using System.Runtime.InteropServices;

namespace JCotton.DismSharp {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public class CustomProperty {
        private string _name;
        private string _value;
        private string _path;

        public string Name => this._name;
        public string Value => this._value;
        public string Path => this._path;
    }
}
