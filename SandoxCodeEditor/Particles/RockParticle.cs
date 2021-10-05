using SandboxCodeEditor.Math;
using SandboxCodeEditor.World;

namespace SandboxCodeEditor.Particles
{
    public class RockParticle : Particle
    {
        private float seed;

        public RockParticle(int x, int y) : base(x, y)
        {
            seed = MathUtilities.RandomValue();
            Color = new SandboxCodeEditor.Renderer.Pixel(123, 138, 145);
        }

        public override void Simulate(float deltaTime)
        {
        }
    }
}