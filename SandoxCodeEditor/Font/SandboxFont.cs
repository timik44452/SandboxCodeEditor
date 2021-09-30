using Newtonsoft.Json;
using SandboxCodeEditor.Font;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SandboxCodeEditor
{
    public class SandboxFont
    {
        public int GlyphSize { get; }
        public string Name { get; }

        private Dictionary<char, SandboxGlyph> glyphs;

        public SandboxFont(string fontName)
        {
            Name = fontName;

            glyphs = new Dictionary<char, SandboxGlyph>();

            if (fontName == "example")
            {
                foreach (var glyph in GlyphExamples.Glyphs)
                {
                    glyphs.Add(glyph.Symbol, glyph);
                }
            }
            else
            {
                LoadGlyphs(fontName);
            }

            if (glyphs.Values.Count > 0)
            {
                GlyphSize = glyphs.Values.First().Size;
            }
        }

        public SandboxGlyph GetGlyph(char symbol)
        {
            return glyphs.TryGetValue(symbol, out SandboxGlyph value)
                ? value
                : null;
        }

        private void LoadGlyphs(string fontName)
        {
            FontData data = new FontData()
            {
                GlyphSize = 32
            };

            data.glyphs.Add(new GlyphData()
            {
                Symbol = 'A',
                X = 21,
                Y = 25
            });

            data.glyphs.Add(new GlyphData()
            {
                Symbol = 'B',
                X = 93,
                Y = 25
            });

            data.glyphs.Add(new GlyphData()
            {
                Symbol = 'B',
                X = 168,
                Y = 25
            });

            //TODO: Add conts
            if (!Directory.Exists(@"Fonts"))
            {
                Directory.CreateDirectory(@"Fonts");
            }

            if (File.Exists(GetMetaPath(fontName)) && File.Exists(GetFontImagePath(fontName)))
            {
                var fontData = new FontData();
                var image = new Bitmap(GetFontImagePath(fontName));

                using (var reader = new StreamReader(GetMetaPath(fontName)))
                {
                    using (var jsonreader = new JsonTextReader(reader))
                    {
                        var serializer = JsonSerializer.Create();
                        fontData = serializer.Deserialize<FontData>(jsonreader);
                    }
                }

                foreach (var glyph in fontData.glyphs)
                {
                    SandboxGlyph sandboxGlyph = new SandboxGlyph(fontData.GlyphSize, glyph.Symbol);

                    for (int y = 0; y < sandboxGlyph.Size; y++)
                    {
                        for (int x = 0; x < sandboxGlyph.Size; x++)
                        {
                            var pixel = image.GetPixel(x + glyph.X, y + glyph.Y);

                            if (pixel.R < 128)
                            {
                                sandboxGlyph.Data[x + y * sandboxGlyph.Size] = 1;
                            }
                        }
                    }

                    glyphs.Add(glyph.Symbol, sandboxGlyph);
                }
            }
        }

        private string GetMetaPath(string fontName)
        {
            return $"Fonts/{fontName}.json";
        }

        private string GetFontImagePath(string fontName)
        {
            return $"Fonts/{fontName}.jpg";
        }
    }
}