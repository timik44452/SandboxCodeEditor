using SandboxCodeEditor.Math;
using SandboxCodeEditor.Renderer;
using SandboxCodeEditor.World;
using System;

namespace SandboxCodeEditor.Particles
{
    public class IronParticle : Particle
    {
        private float seed;
        private float rustTime;

        private DateTime bornTime;
        private Pixel metalColor;
        private Pixel rustColor;

        private const float minimalRustTime = 8;
        private const float rustTimeRange = 10;

        public IronParticle(int x, int y) : base(x, y)
        {
            seed = MathUtilities.RandomValue();
            metalColor = new Pixel(127, 138, 144);
            rustColor = new Pixel(160, 110, 120);
            bornTime = DateTime.Now;
            rustTime = minimalRustTime + seed * rustTimeRange;

            Color = metalColor;
        }

        public override void Simulate(float deltaTime)
        {
            float localTime = (float)(DateTime.Now - bornTime).TotalSeconds;
            float lerpTime = 1 - ((rustTime - localTime) / rustTime);

            if (localTime > rustTime)
            {
                Position += (Physics.Gravity + WorldEnviropment.Wind) * deltaTime;
            }

            Color = Pixel.Lerp(metalColor, rustColor, lerpTime);
        }
    }
}