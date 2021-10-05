using SandboxCodeEditor.Math;
using SandboxCodeEditor.Renderer;
using SandboxCodeEditor.World;
using System;

namespace SandboxCodeEditor.Particles
{
    public class BombParticle : Particle
    {
        private float seed;
        private float power;
        private Vector2 direction;

        private DateTime bornTime;
        private Pixel bombColor;
        private Pixel flameAColor;
        private Pixel flameBColor;

        private float blinkDuration = 0.5F;
        private float explosionTimeout = 5;
        private float explosionLifetime = 1;

        public BombParticle(int x, int y, float bombPower = 1F, float duration = 1F) : base(x, y)
        {
            seed = MathUtilities.RandomValue();
            power = bombPower;
            bombColor = new Pixel(250, 5, 5);
            flameAColor = new Pixel(255, 211, 3);
            flameBColor = new Pixel(227, 63, 0);
            bornTime = DateTime.Now;

            Color = flameAColor;
            direction = new Vector2(
                (float)System.Math.Sin(seed * 6.18F),
                (float)System.Math.Cos(seed * 6.18F));

            explosionTimeout *= duration;
        }

        public override void Simulate(float deltaTime)
        {
            float localTime = (float)(DateTime.Now - bornTime).TotalSeconds;

            if (localTime < explosionTimeout)
            {
                Color = (int)(localTime / blinkDuration) % 2 == 0
                    ? bombColor
                    : Pixel.White;
            }
            else
            {
                float explosionTime = MathUtilities.Clamp((localTime - explosionTimeout) / explosionLifetime);

                Color = Pixel.Lerp(flameAColor, flameBColor, explosionTime);

                Position += direction * (1F - explosionTime) * deltaTime * power * 0.1F;

                if(explosionTime > 0.99F)
                {
                    Destroy();
                }
                else if(explosionTime < 0.65F)
                {
                    int x = (int)Position.X;
                    int y = (int)Position.Y;

                    GridWorld.World.GetParticle(x - 1, y + 1)?.Destroy(this);
                    GridWorld.World.GetParticle(x, y + 1)?.Destroy(this);
                    GridWorld.World.GetParticle(x + 1, y + 1)?.Destroy(this);
                    GridWorld.World.GetParticle(x - 1, y)?.Destroy(this);
                    GridWorld.World.GetParticle(x + 1, y)?.Destroy(this);
                    GridWorld.World.GetParticle(x - 1, y - 1)?.Destroy(this);
                    GridWorld.World.GetParticle(x, y - 1)?.Destroy(this);
                    GridWorld.World.GetParticle(x + 1, y - 1)?.Destroy(this);
                }
            }
        }
    }
}