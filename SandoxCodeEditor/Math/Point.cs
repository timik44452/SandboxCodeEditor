namespace SandboxCodeEditor.Math
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point operator * (Point a, int b)
        {
            a.X *= b;
            a.Y *= b;

            return a;
        }

        public static Point operator +(Point a, Point b)
        {
            a.X += b.X;
            a.Y += b.Y;

            return a;
        }

        public override string ToString()
        {
            return $"({X} {Y})";
        }

        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}