using SandboxCodeEditor.Math;
using System;

namespace SandboxCodeEditor.Renderer
{
    public struct Pixel
    {
        public byte R, G, B, A;
        public static Pixel White { get { return new Pixel(255, 255, 255); } }
        public static Pixel Gray { get { return new Pixel(127, 127, 127); } }
        public static Pixel Black { get => black; }


        private static Pixel black = new Pixel(0, 0, 0);

        public Pixel(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Pixel(float r, float g, float b)
        {
            R = (byte)(r * 255);
            G = (byte)(g * 255);
            B = (byte)(b * 255);
            A = 255;
        }

        public Pixel(int r, int g, int b, int a)
        {
            R = (byte)r;
            G = (byte)g;
            B = (byte)b;
            A = (byte)a;
        }
        public Pixel(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }

        public static Pixel Lerp(Pixel from, Pixel to, float lerpTime)
        {
            lerpTime = MathUtilities.Clamp(lerpTime);

            from.R = (byte)(from.R + (to.R - from.R) * lerpTime);
            from.G = (byte)(from.G + (to.G - from.G) * lerpTime);
            from.B = (byte)(from.B + (to.B - from.B) * lerpTime);
            from.A = (byte)(from.A + (to.A - from.A) * lerpTime);

            return from;
        }

        public static Pixel operator *(Pixel a, float b)
        {
            return new Pixel((byte)(a.R * b), (byte)(a.G * b), (byte)(a.B * b), a.A);
        }
        public void FROMRGB(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public int ToRGB()
        {
            return
                R << 16 |
                G << 8 |
                B;
        }
    }
}
