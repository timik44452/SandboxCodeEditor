using SandboxCodeEditor.Fonts;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SandboxCodeEditor
{
    public class SandboxFont
    {
        public int Size { get; }
        public string Name { get; }

        private Dictionary<char, SandboxGlyph> glyphs;

        private const string FontsDrectory = @"Fonts";

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
                Size = glyphs.Values.First().Height;
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
            //TODO: Add conts
            if (!Directory.Exists(FontsDrectory))
            {
                Directory.CreateDirectory(FontsDrectory);
            }

            LoadTTF(fontName);
        }

        private void LoadTTF(string fontName)
        {
            var path = GetFontPath(fontName);

            if (File.Exists(path))
            {
                string nullLoaded = string.Empty;

                using(var loader = new FontLoader(path))
                {
                    var chars = Enumerable.Range(0, 256)
                      .Select(i => (char)i)
                      .Where(c => !char.IsControl(c));

                    foreach (var _char in chars)
                    {
                        var glyph = loader.GetGlyph(_char);

                        if (glyph == null)
                        {
                            nullLoaded += $"{glyph}\n";
                        }
                        else
                        {
                            glyphs.Add(_char, glyph);
                        }
                    }

                    MessageBox.Show(nullLoaded);
                }
            }
        }
        

        private string GetFontPath(string fontName)
        {
            return $"{FontsDrectory}/{fontName}.ttf";
        }
    }
}