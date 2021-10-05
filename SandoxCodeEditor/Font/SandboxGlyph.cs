namespace SandboxCodeEditor
{
    public class SandboxGlyph
    {
        public int Width { get; }
        public int Height { get; }
        public char Symbol { get; }
        public byte[,] Data { get; }

        public SandboxGlyph(int widht, int height, char symbol)
        {
            Data = new byte[widht, height];
            Symbol = symbol;
            Width = widht;
            Height = height;
        }
    }
}
