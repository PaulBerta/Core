using System;

namespace Assets.Scripts.Utils
{
    public static class MathUtility
    {
        public static float Sigmoid(float value)
        {
            return 1.0f / (1.0f + (float)Math.Exp(value));
        }
        
        public static float Parabola (float x, float alfa, float beta)
        {
            return alfa * x * x + beta;
        }
    }

}
