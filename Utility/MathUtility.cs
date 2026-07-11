using System;

namespace SPTMapProgression.Utility;

public static class MathUtility
{
    public static double EaseInOutCirc(double x)
    {
        return x < 0.5
            ? (1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2
            : (Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2;
    }

    public static double EaseInSine(double x)
    {
        return 1 - Math.Cos((x * Math.PI) / 2);
    }
    
    
}