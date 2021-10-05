using SandboxCodeEditor.Properties;
using System.Collections.Generic;

namespace SandboxCodeEditor
{
    public static class GlyphExamples
    {
        public static List<SandboxGlyph> Glyphs { get; }

        static GlyphExamples()
        {
            var size = 5;
            var charsOnImage = new char[]
            {
                'A',
                'B',
                'C',
                'D',
                'E',
                'F',
                'G',
                'H',
                'I',
                'J',
                'K',
                'L',
                'M',
                'N',
                'O',
                'P',
                'Q',
                'R',
                'S',
                'T',
                'U',
                'V',
                'W',
                'X',
                'Y',
                'Z',
                '?',
                '!',
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9',
                '0',
                '{',
                '}',
                '[',
                ']',
                '.'
            };

            Glyphs = new List<SandboxGlyph>();

            for (int charNumber = 0; charNumber < charsOnImage.Length; charNumber++)
            {
                int posX = charNumber * size % Resources.font.Width;
                int posY = charNumber * size / Resources.font.Width * size + charNumber / 4;

                SandboxGlyph glyph = new SandboxGlyph(size, size, charsOnImage[charNumber]);

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        var pixel = Resources.font.GetPixel(x + posX, y + posY);

                        if (pixel.R < 128)
                        {
                            glyph.Data[x, y] = 1;
                        }
                    }
                }
                Glyphs.Add(glyph);
            }
        }
    }
}