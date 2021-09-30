using SandboxCodeEditor.Editor;
using SandboxCodeEditor.Renderer;

namespace SandboxCodeEditor.Widgets
{
    public class ModeWidget : Widget
    {
        private Pixel modeColor = new Pixel(0, 180, 180);
        private EditorController controller;

        public ModeWidget(int x, int y, int width, int height, EditorController controller) : base(x, y, width, height)
        {
            this.controller = controller;

            Handler(this, controller.EditorMode);

            controller.OnModeChanged += Handler;
        }

        private void Handler(object sender, Mode e)
        {
            Clear();
            switch (e)
            {
                case Mode.Command:
                    {
                        CreateGlyph(0, controller.Font.GetGlyph('C'), modeColor);
                        CreateGlyph(1, controller.Font.GetGlyph('M'), modeColor);
                        CreateGlyph(2, controller.Font.GetGlyph('D'), modeColor);
                    }
                    break;

                case Mode.Text:
                    {
                        CreateGlyph(0, controller.Font.GetGlyph('T'), modeColor);
                        CreateGlyph(1, controller.Font.GetGlyph('X'), modeColor);
                        CreateGlyph(2, controller.Font.GetGlyph('T'), modeColor);
                    }
                    break;
            }
        }

        private void CreateGlyph(int index, SandboxGlyph glyph, Pixel color)
        {
            if (glyph == null)
            {
                return;
            }

            int offset = index * controller.GetGlyphWidth();

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
                                SetColor(x * controller.Settings.PixelSize + _x + offset, y * controller.Settings.PixelSize + _y, color);
                            }
                    }
                }
            }
        }
    }
}