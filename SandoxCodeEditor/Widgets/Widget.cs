using SandboxCodeEditor.Math;
using SandboxCodeEditor.Renderer;
using System;

namespace SandboxCodeEditor.Widgets
{
    public abstract class Widget
    {
        public bool IsDrawing { get; set; } = true;
        public Region Rect { get; private set; }
        private int[,] mask;
        private int[,] colors;

        public Widget(int x, int y, int width, int height)
        {
            Rect = new Region(x, y, width, height);
            mask = new int[width, height];
            colors = new int[width, height];
        }

        public void Move(int x, int y)
        {
            Rect = new Region(x, y, Rect.Width, Rect.Height);
        }

        public void Draw(RendererContext context)
        {
            OnBeforeDrawing();

            for (int y = 0; y < Rect.Height; y++)
                for (int x = 0; x < Rect.Width; x++)
                {
                    int posX = Rect.X + x;
                    int posY = Rect.Y + y;

                    if (mask[x, y] > 0 && context.Rect.Contains(posX, posY))
                    {
                        context.Buffer[posX + posY * context.Rect.Width] = colors[x, y];
                    }
                }
        }

        public virtual void Update()
        {
        }

        protected void SetColor(int x, int y, Pixel color)
        {
            if (Rect.Contains(x + Rect.X, y + Rect.Y))
            {
                mask[x, y] = 1;
                colors[x, y] = color.ToRGB();
            }
        }

        protected void Clear()
        {
            for (int y = 0; y < Rect.Height; y++)
                for (int x = 0; x < Rect.Width; x++)
                    mask[x, y] = 0;
        }

        protected virtual void OnBeforeDrawing()
        {

        }
    }
}