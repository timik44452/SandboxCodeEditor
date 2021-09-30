using SandboxCodeEditor.Math;

namespace SandboxCodeEditor
{
    public class RendererContext
    {
        public int[] Buffer;

        public Region Rect { get; private set; } 

        public RendererContext(int Width, int Height)
        {
            Buffer = new int[Width * Height];
            Rect = new Region(0, 0, Width, Height);
        }

        public void ResetBuffers(int value = 0)
        {
            for (int i = 0; i < Buffer.Length; i++)
            {
                Buffer[i] = value;
            }
        }
    }
}
