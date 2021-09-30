using SandboxCodeEditor.Math;
using SandboxCodeEditor.Particles;
using System.Collections.Generic;

namespace SandboxCodeEditor.World
{
    public class GridWorld
    {
        public static GridWorld World { get; private set; }

        private Region rect;

        //TODO: Add pointer buffer
        public List<Particle> Particles { get; }

        private int[,] grid;

        public GridWorld(int width, int height)
        {
            rect = new Region(0, 0, width, height);
            grid = new int[width, height];
            Particles = new List<Particle>();
            World = this;

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    grid[x, y] = -1;
                }
        }

        public void Simulate(float deltaTime)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particle particle = Particles[i];

                if (particle == null)
                    continue;

                Point prevParticlePos = particle.Position;
                particle.Simulate(deltaTime);
                Point currParticlePos = particle.Position;

                grid[prevParticlePos.X, prevParticlePos.Y] = -1;

                if (rect.Contains(currParticlePos.X, currParticlePos.Y) && !particle.IsDestroyed)
                {
                    grid[currParticlePos.X, currParticlePos.Y] = i;
                }
                else
                {
                    Particles[i] = null;
                }
            }
        }

        public void InstantiateParticle(Particle particle)
        {
            if (rect.Contains(particle.Position))
            {
                Point point = particle.Position;

                if (grid[point.X, point.Y] < 0)
                {
                    grid[point.X, point.Y] = Particles.Count;
                    Particles.Add(particle);
                }
            }
        }

        public bool IsEmptyPlace(Point point)
        {
            return IsEmptyPlace(point.X, point.Y);
        }

        public bool IsEmptyPlace(int x, int y)
        {
            return !rect.Contains(x, y) || grid[x, y] < 0;
        }

        public Particle GetParticle(int x, int y)
        {
            if (rect.Contains(x, y) && grid[x, y] >= 0)
            {
                return Particles[grid[x, y]];
            }

            return null;
        }
    }
}