namespace SandboxCodeEditor
{
    public class SandboxGlyph
    {
        public int Size { get; }
        public char Symbol { get; }
        public byte[] Data { get; }

        public SandboxGlyph(int size, char symbol)
        {
            Data = new byte[size * size];
            Symbol = symbol;
            Size = size;
        }

        public SandboxGlyph(char symbol, byte[] data)
        {
            var size = System.Math.Sqrt(data.Length);

            Size = (int)size;
            Symbol = symbol;
            Data = data;

            if (Size != size)
            {
                throw new System.ArgumentException();
            }
        }
    }
}
