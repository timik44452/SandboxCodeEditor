using SandboxCodeEditor.Math;
using SandboxCodeEditor.World;

namespace SandboxCodeEditor.Particles
{
    public class SandParticle : Particle
    {
        private float seed;
        private float localTime;

        public SandParticle(int x, int y) : base(x, y)
        {
            seed = MathUtilities.RandomValue();
            Color = new SandboxCodeEditor.Renderer.Pixel(255, 231, 165);
        }

        public override void Simulate(float deltaTime)
        {
            if (localTime > seed * 4)
            {
                Position += (Physics.Gravity + WorldEnviropment.Wind) * deltaTime * 0.1F;
            }

            localTime += deltaTime;
        }
    }
}