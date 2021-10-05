using SandboxCodeEditor.Renderer;

namespace SandboxCodeEditor.Editor
{
    public class EditorSettings
    {
        public int Borders = 5;
        public int SpaceBetweenGlyps = 0;
        public int SpaceBetweenLines = 2;
        public int PixelSize = 1;
        public Pixel CursoreColor = new Pixel(2, 199, 193);
        public Pixel BackgroundColor = new Pixel(41, 40, 56);
        public Pixel PhantomColor = new Pixel(59, 117, 115);
    }
}