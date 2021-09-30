using SandboxCodeEditor.Editor;
using SandboxCodeEditor.Math;
using System;

namespace SandboxCodeEditor.Widgets
{
    public class CursoreWidget : Widget
    {
        private EditorController controller;

        public CursoreWidget(EditorController controller) : base(0, 0, 2, controller.GetGlyphHeight() * controller.Settings.PixelSize)
        {
            this.controller = controller;
            controller.OnCursorMoved += (e, p) =>
            {
                Update(controller.TextToScreenPosition(p));
            };

            Draw();
            Update(controller.TextToScreenPosition(controller.CursorePosition));
        }

        private void Draw()
        {
            for (int y = 0; y < Rect.Height; y++)
                for (int x = 0; x < Rect.Width; x++)
                {
                    SetColor(x, y, controller.Settings.CursoreColor);
                }
        }

        private void Update(Point point)
        {
            Move(point.X * controller.Settings.PixelSize, point.Y * controller.Settings.PixelSize);
        }

        public override void Update()
        {
            IsDrawing = DateTime.Now.Millisecond < 500;
        }
    }
}