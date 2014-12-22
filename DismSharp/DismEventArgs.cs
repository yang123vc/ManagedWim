using System;

namespace JCotton.DismSharp {
    public class DismEventArgs : EventArgs {
        public long _current;
        public long _total;

        public long Current => this._current;
        public long Total => this._total;

        public DismEventArgs(long current, long total) {
            this._current = current;
            this._total = total;
        }
    }
}
