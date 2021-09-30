using SandboxCodeEditor.Editor;
using SandboxCodeEditor.Renderer;

namespace SandboxCodeEditor.Widgets
{
    public class CodeLineWidget : Widget
    {
        //private Pixel modeColor = new Pixel(0, 180, 180);
        private EditorController controller;

        public CodeLineWidget(int height, EditorController controller) : base(0, 0, (controller.GetScreenCharsWidth() + controller.Settings.Borders) * 2, height)
        {
            this.controller = controller;
            Draw();
        }

        private void Draw()
        {
            for (int line = 0; line < 16; line++)
            {
                string lineTxt = $"{line}";

                for (int i = 0; i < lineTxt.Length; i++)
                {
                    CreateGlyph(i, line, controller.Font.GetGlyph(lineTxt[i]), controller.Settings.CursoreColor);
                }
            }
        }

        private void CreateGlyph(int charInder, int lineIndex, SandboxGlyph glyph, Pixel color)
        {
            if (glyph == null)
            {
                return;
            }

            var point = controller.TextToScreenPosition(lineIndex, charInder);

            for (int y = 0; y < glyph.Size; y++)
            {
                for (int x = 0; x < glyph.Size; x++)
                {
                    int i = x + y * glyph.Size;

                    if (glyph.Data[i] > 0)
                    {
                        for (int _x = 0; _x < controller.Settings.PixelSize; _x++)
                            for (int _y = 0; _y < controller.Settings.PixelSize; _y++)
                            {
                                SetColor((x + point.X) * controller.Settings.PixelSize + _x , (y + point.Y) * controller.Settings.PixelSize + _y, color);
                            }
                    }
                }
            }
        }
    }
}