using System;
using System.Runtime.InteropServices;

namespace JCotton.DismSharp.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct DismImageInfo {
        ImageType ImageType;
        uint ImageIndex;
        string ImageName;
        string ImageDescription;
        ulong ImageSize;
        uint Architecture;
        string ProductName;
        string EditionId;
        string InstallationType;
        string Hal;
        string ProductType;
        string ProductSuite;
        uint MajorVersion;
        uint MinorVersion;
        uint Build;
        uint SpBuild;
        uint SpLevel;
        ImageBootable Bootable;
        string SystemRoot;
        IntPtr Language;
        uint LanguageCount;
        uint DefaultLanguageIndex;
        IntPtr CustomizedInfo;
    }
}