namespace SandboxCodeEditor.Math
{
    public struct Region
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Right { get; }
        public int Lefth { get; }
        public int Top { get; }
        public int Bottom { get; }

        public Region(int X, int Y, int Width, int Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;

            Lefth = X;
            Right = X + Width;

            Top = Y;
            Bottom = Y + Height;
        }

        public bool Contains(int X, int Y)
        {
            return
                X >= Lefth && Y >= Top &&
                X < Right && Y < Bottom;
        }

        public bool Contains(Vector2 point)
        {
            return
                point.X >= Lefth && point.Y >= Top &&
                point.X < Right && point.Y < Bottom;
        }
    }
}
