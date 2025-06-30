using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoResService
{
    using System;
    using System.Runtime.InteropServices;

    public class DisplayManager
    {
        public const int ENUM_CURRENT_SETTINGS = -1;
        private const int CDS_UPDATEREGISTRY = 0x01;
        private const int CDS_RESET = 0x40000000;
        private const int DISP_CHANGE_SUCCESSFUL = 0;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 32;
            private const int CCHFORMNAME = 32;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public ushort dmSpecVersion;
            public ushort dmDriverVersion;
            public ushort dmSize;
            public ushort dmDriverExtra;
            public uint dmFields;

            public int dmPositionX;
            public int dmPositionY;
            public uint dmDisplayOrientation;
            public uint dmDisplayFixedOutput;

            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;

            public ushort dmLogPixels;
            public uint dmBitsPerPel;
            public uint dmPelsWidth;
            public uint dmPelsHeight;
            public uint dmDisplayFlags;
            public uint dmDisplayFrequency;

            public uint dmICMMethod;
            public uint dmICMIntent;
            public uint dmMediaType;
            public uint dmDitherType;
            public uint dmReserved1;
            public uint dmReserved2;

            public uint dmPanningWidth;
            public uint dmPanningHeight;
        }

        [DllImport("user32.dll")]
        private static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        public static void ApplyResolution(int width, int height)
        {
            DEVMODE devMode = new DEVMODE();
            devMode.dmSize = (ushort)Marshal.SizeOf(typeof(DEVMODE));

            if (!EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devMode))
            {
                throw new InvalidOperationException("Cannot get current display settings.");
            }

            devMode.dmPelsWidth = (uint)width;
            devMode.dmPelsHeight = (uint)height;
            devMode.dmFields = 0x00080000 | 0x00100000; // DM_PELSWIDTH | DM_PELSHEIGHT

            int result = ChangeDisplaySettings(ref devMode, CDS_UPDATEREGISTRY | CDS_RESET);
            if (result != DISP_CHANGE_SUCCESSFUL)
            {
                throw new InvalidOperationException("Resolution change failed with code: " + result);
            }
        }
    }


}
