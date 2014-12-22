using System;
using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismImageInfo {
        public ImageType ImageType;
        public uint ImageIndex;
        public string ImageName;
        public string ImageDescription;
        public ulong ImageSize;
        public WindowsImageArchitecture Architecture;
        public string ProductName;
        public string EditionId;
        public string InstallationType;
        public string Hal;
        public string ProductType;
        public string ProductSuite;
        public uint MajorVersion;
        public uint MinorVersion;
        public uint Build;
        public uint SpBuild;
        public uint SpLevel;
        public ImageBootable Bootable;
        public string SystemRoot;
        public IntPtr Language;
        public uint LanguageCount;
        public uint DefaultLanguageIndex;
        public IntPtr CustomizedInfo;
    }
}