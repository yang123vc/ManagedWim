using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using JCotton.WimSharp.Interop;

namespace JCotton.WimSharp {
    public class WimFile {
        public long SizeBytes { get; private set; }
        public WimImage[] Images { get; private set; }
        public string Path { get; private set; }
        public Guid Guid { get; private set; }
        public WimCompressionType CompressionType { get; private set; }
        public ushort PartNumber { get; private set; }
        public ushort TotalParts { get; private set; }
        public uint BootIndex { get; private set; }
        public WimFileAttributes Attributes { get; private set; }
        public WimCreationOptions CreationOptions { get; private set; }

        public WimFile(IntPtr handle) {
            IntPtr ppvImageInfo;
            uint pcbImageInfo;
            if(!NativeMethods.WIMGetImageInformation(handle, out ppvImageInfo, out pcbImageInfo)) {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
            // dunno why, but there's a garbage char at the start (BOM?)
            string rawXml = Marshal.PtrToStringUni(ppvImageInfo).Substring(1);
            if(NativeMethods.LocalFree(ppvImageInfo) != IntPtr.Zero) {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
            XDocument xml = XDocument.Parse(rawXml);
            this.SizeBytes = Convert.ToInt64(xml.Element("WIM").Element("TOTALBYTES").Value);
            WIM_INFO tmp;
            NativeMethods.WIMGetAttributes(handle, out tmp, (uint)Marshal.SizeOf<WIM_INFO>());
            this.Path = tmp.WimPath;
            this.Guid = tmp.Guid;
            this.CompressionType = tmp.CompressionType;
            this.PartNumber = tmp.PartNumber;
            this.TotalParts = tmp.TotalParts;
            this.BootIndex = tmp.BootIndex;
            this.Attributes = tmp.WimAttributes;
            this.CreationOptions = tmp.WimFlagsAndAttr;
            this.Images = xml.Element("WIM")
                             .Elements("IMAGE")
                             .Select(img => new WimImage() {
                                 DirectoryCount = Convert.ToInt64(img.Element("DIRCOUNT").Value),
                                 FileCount = Convert.ToInt64(img.Element("FILECOUNT").Value),
                                 TotalBytes = Convert.ToInt64(img.Element("TOTALBYTES").Value),
                                 HardlinkBytes = Convert.ToInt64(img.Element("HARDLINKBYTES").Value),
                                 Index = Convert.ToInt32(img.Attribute("INDEX").Value),
                                 CreationTime = DateTime.FromFileTime(
                                     Convert.ToInt64(img.Element("CREATIONTIME").Element("HIGHPART").Value, 16) << 32 |
                                         Convert.ToInt64(img.Element("CREATIONTIME").Element("LOWPART").Value, 16)
                                     ),
                                 LastModificationTime = DateTime.FromFileTime(
                                     Convert.ToInt64(
                                         img.Element("LASTMODIFICATIONTIME").Element("HIGHPART").Value, 16) << 32 |
                                         Convert.ToInt64(
                                             img.Element("LASTMODIFICATIONTIME").Element("LOWPART").Value,
                                             16
                                             )
                                     ),
                                 IsWimBoot = Convert.ToBoolean(Convert.ToInt32(img.Element("WIMBOOT").Value))
                             }).ToArray();
        }

    }
}
