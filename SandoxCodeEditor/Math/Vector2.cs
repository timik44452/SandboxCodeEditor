namespace SandboxCodeEditor.Math
{
    public struct Vector2
    {
        public float X
        {
            get => x;
            set
            {
                x = value;
                Recalculate();
            }
        }

        public float Y
        {
            get => y;
            set
            {
                y = value;
                Recalculate();
            }
        }

        public float Length { get => length; }
        public float Summ { get => summ; }
        public float Dot { get => dot; }

        public static Vector2 Zero { get => new Vector2(); }
        public static Vector2 Up { get => new Vector2(0, -1); }

        private float
            x, y,
            summ, dot,
            length;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;

            summ = dot = length = 0;

            Recalculate();
        }

        public static void Swap(ref Vector2 a, ref Vector2 b)
        {
            Vector2 buffer = a;
            a = b;
            b = buffer;
        }

        public static Vector2 operator +(Vector2 a,Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }

        public static implicit operator Point (Vector2 value)
        {
            return new Point((int)value.x, (int)value.y);
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float alpha)
        {
            a.x += (b.x - a.x) * alpha;
            a.y += (b.y - a.y) * alpha;

            a.Recalculate();

            return a;
        }
        public override string ToString()
        {
            return $"({x};{y})";
        }

        private void Recalculate()
        {
            summ = x + y;
            dot = x * y;
            length = (float)System.Math.Sqrt(x * x + y * y);
        }
    }
}
