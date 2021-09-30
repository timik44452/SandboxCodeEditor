using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace SandboxCodeEditor.Renderer
{
    public class Texture
    {
        private Pixel[] texture = new Pixel[0];
        private int w = 0, h = 0;

        public Texture()
        { }
        public Texture(int Width,int Height)
        {
            w = Width;
            h = Height;

            texture = new Pixel[w * h];
        }
        public Texture(string path)
        {
            LoadTexture(path);
        }
        public void LoadTexture(string path)
        {
            Bitmap image = new Bitmap(path);
            w = image.Width; h = image.Height;
            texture = new Pixel[w * h * 4];
            BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            
            byte[] buffer = new byte[w * h * 4];
            Marshal.Copy(data.Scan0, buffer, 0, w * h * 4);

            for (int ids = 0; ids < w * h * 4; ids += 4)
                texture[ids] = new Pixel(buffer[ids + 2], buffer[ids + 1], buffer[ids], buffer[ids + 3]);

            image.UnlockBits(data);
            image.Dispose();
        }

        public Pixel GetUVCOLOR(float u, float v)
        {
            if (texture.Length == 0)
                return new Pixel(255, 255, 255);

            int x = (int)(u * (w - 1)), y = (int)(v * (h - 1));
            int ids = x * 4 + y * 4 * w;
            return texture[ids];
        }
    }
}
