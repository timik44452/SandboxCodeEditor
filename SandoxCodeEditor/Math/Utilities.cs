using System;

namespace SandboxCodeEditor.Math
{
    public static class Utilities
    {
        private static Random random;

        static Utilities()
        {
            random = new Random();
        }

        public static float RandomValue()
        {
            return (float)random.NextDouble();
        }

        public static float Max(float valueA, float valueB)
        {
            return valueA > valueB ? valueA : valueB;
        }

        public static float Min(float valueA, float valueB)
        {
            return valueA < valueB ? valueA : valueB;
        }

        public static float Floor(float value)
        {
            return (int)value;
        }

        public static float Repeate(float value, float time)
        {
            return value - (int)(value / time) * time;
        }

        public static float Clamp(float lerpTime)
        {
            if (lerpTime < 0)
                return 0;
            else if (lerpTime > 1)
                return 1;

            return lerpTime;
        }
    }
}
