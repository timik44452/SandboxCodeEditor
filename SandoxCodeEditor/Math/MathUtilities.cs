using System;

namespace SandboxCodeEditor.Math
{
    public static class MathUtilities
    {
        private static Random random;

        static MathUtilities()
        {
            random = new Random();
        }

        public static float RandomValue()
        {
            return (float)random.NextDouble();
        }

        public static int Max(int valueA, int valueB)
        {
            return valueA > valueB ? valueA : valueB;
        }

        public static int Min(int valueA, int valueB)
        {
            return valueA < valueB ? valueA : valueB;
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
