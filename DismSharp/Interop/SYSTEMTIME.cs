using System;
using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;

        public DateTime ToDateTime() => new DateTime(
            this.wYear,
            this.wMonth,
            this.wDay,
            this.wHour,
            this.wMinute,
            this.wSecond,
            this.wMilliseconds,
            DateTimeKind.Local
            );
    }
}