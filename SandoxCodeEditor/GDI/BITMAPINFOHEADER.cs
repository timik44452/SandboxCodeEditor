using System.Runtime.InteropServices;

namespace SandboxCodeEditor.GDI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFOHEADER
    {
        public int bihSize;
        public int bihWidth;
        public int bihHeight;
        public short bihPlanes;
        public short bihBitCount;
        public int bihCompression;
        public int bihSizeImage;
        public double bihXPelsPerMeter;
        public double bihClrUsed;
    }
}
