using System;
using System.Globalization;

namespace SPTMapProgression.Utility;

public static class MathUtility
{
    public static double EaseInOutCirc(double x)
    {
        return x < 0.5
            ? (1 - Math.Sqrt(1 - Math.Pow(2 * x, 2))) / 2
            : (Math.Sqrt(1 - Math.Pow(-2 * x + 2, 2)) + 1) / 2;
    }

    public static string GetFormattedNumber(int number)
    {
        return "₽" + number.ToString("N0");
    }
}