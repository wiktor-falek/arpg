using System;
using System.Numerics;
using Microsoft.Xna.Framework;

public static class Utils
{
    public static double CalculateAngle(
        Microsoft.Xna.Framework.Vector2 a,
        Microsoft.Xna.Framework.Vector2 b
    )
    {
        float deltaX = b.X - a.X;
        float deltaY = b.Y - a.Y;
        double angle = Math.Atan2(deltaY, deltaX);
        return angle;
    }

    public static double CalculateAngleInDegrees(
        Microsoft.Xna.Framework.Vector2 a,
        Microsoft.Xna.Framework.Vector2 b
    )
    {
        float deltaX = b.X - a.X;
        float deltaY = b.Y - a.Y;
        double angle = Math.Atan2(deltaY, deltaX);
        double angleInDegrees = MathHelper.ToDegrees((float)angle);
        return angleInDegrees;
    }
}
