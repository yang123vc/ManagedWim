using System;
using System.Runtime.InteropServices;
using JCotton.WimSharp.Interop;

namespace JCotton.WimSharp {
    public class WimImage {
        public int Index { get; set; }
        public long DirectoryCount { get; set; }
        public long FileCount { get; set; }
        public long TotalBytes { get; set; }
        public long HardlinkBytes { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public bool IsWimBoot { get; set; }
        // add properties for OS information (WINDOWS element)
    }
}
