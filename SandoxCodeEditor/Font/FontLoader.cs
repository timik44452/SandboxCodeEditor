using System;
using System.Drawing;
using System.Drawing.Text;

namespace SandboxCodeEditor.Fonts
{
    public class FontLoader : IDisposable
    {
        private readonly Bitmap output;
        private readonly Graphics outputGraphics;
        private readonly FontFamily family;

        public FontLoader(string path)
        {
            output = new Bitmap(24, 24);
            outputGraphics = Graphics.FromImage(output);

            var fonts = new PrivateFontCollection();
            fonts.AddFontFile(path);
            family = fonts.Families[0];
        }

        public SandboxGlyph GetGlyph(char @char)
        {
            Color background = Color.White;
            Bitmap bitmap = Convert(@char);
            int left = int.MaxValue;
            int top = int.MaxValue;
            int right = int.MinValue;
            int bottom = int.MinValue;

            //TODO: Use fast bitmap
            for (int y = 0; y < bitmap.Height; y++)
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);

                    if (GetMaxDiff(background, color) > 128)
                    {
                        left = Math.MathUtilities.Min(left, x);
                        top = Math.MathUtilities.Min(top, y);
                        right = Math.MathUtilities.Max(right, x);
                        bottom = Math.MathUtilities.Max(bottom, y);
                    }
                }

            if (left < 0 || top < 0 || right < left || bottom < top)
            {
                return null;
            }

            SandboxGlyph sandboxGlyph = new SandboxGlyph(right - left + 1, bitmap.Height, @char);

            for (int y = 0; y < bitmap.Height; y++)
                for (int x = left; x <= right; x++)
                {
                    Color color = bitmap.GetPixel(x, y);

                    if (GetMaxDiff(background, color) > 128)
                    {
                        sandboxGlyph.Data[x - left, y] = 1;
                    }
                }

            return sandboxGlyph;
        }

        /// <summary>
        /// Converts TTF character to bitmap
        /// </summary>
        /// <param name="fontFileName">
        /// Full name of font file
        /// </param>
        /// <param name="encoding">ASCII, UNICODE ...</param>
        /// <param name="color">Color to draw given character</param>
        /// <returns></returns>
        private Bitmap Convert(char @char)
        {
            outputGraphics.Clear(Color.White);
            outputGraphics.DrawString(
                $"{@char}",
                new Font(family, 12, FontStyle.Regular),
                Brushes.Black,
                0, 0
                );

            return output;
        }

        /// <summary>
        /// Return similar color
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static byte GetMaxDiff(Color a, Color b)
        {
            byte bDiff = System.Convert.ToByte(
                System.Math.Abs((short)(a.B - b.B)));

            byte gDiff = System.Convert.ToByte(
                System.Math.Abs((short)(a.G - b.G)));

            byte rDiff = System.Convert.ToByte(
                System.Math.Abs((short)(a.R - b.R)));

            return System.Math.Max(rDiff, System.Math.Max(bDiff, gDiff));
        }

        public void Dispose()
        {
            outputGraphics.Dispose();
            output.Dispose();
            family.Dispose();
        }
    }
}