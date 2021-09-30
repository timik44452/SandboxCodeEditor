using SandboxCodeEditor.Math;

namespace SandboxCodeEditor.World
{
    public static class WorldEnviropment
    {
        public static float Time { get; private set; }
        public static Vector2 Wind { get; private set; }

        static WorldEnviropment()
        {
        }

        public static void Simulate(float deltaTime)
        {
            Time = Utilities.Repeate(Time + deltaTime, 60);
            Wind = new Vector2(0, 0);
        }
    }
}