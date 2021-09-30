using System.Runtime.InteropServices;

namespace SandboxCodeEditor.GDI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFO
    {
        public BITMAPINFOHEADER biHeader;
        public int biColors;
    }
}
