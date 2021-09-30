using SandboxCodeEditor.Math;
using SandboxCodeEditor.Renderer;
using SandboxCodeEditor.World;

namespace SandboxCodeEditor.Particles
{
    public abstract class Particle
    {
        public bool IsDestroyed { get; private set; }

        public Pixel Color { get; set; }
        public Vector2 Position 
        {
            get => position;
            set
            {
                Point newPosition = value;
                if (GridWorld.World.IsEmptyPlace(newPosition))
                {
                    position = value;
                }
            }
        }

        private Vector2 position;
        
        public Particle(int x, int y)
        {
            Position = new Vector2(x, y);
        }

        public abstract void Simulate(float deltaTime);

        public void Destroy()
        {
            IsDestroyed = true;
        }

        public void Destroy(BombParticle bombParticle)
        {
            if (bombParticle.GetType() != GetType())
                IsDestroyed = true;
        }
    }
}
