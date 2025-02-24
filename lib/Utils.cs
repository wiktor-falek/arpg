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
        return Math.Atan2(deltaY, deltaX);
    }

    public static double CalculateAngleInDegrees(
        Microsoft.Xna.Framework.Vector2 a,
        Microsoft.Xna.Framework.Vector2 b
    )
    {
        double angle = CalculateAngle(a, b);
        return MathHelper.ToDegrees((float)angle);
    }

    public static Microsoft.Xna.Framework.Vector2 GetRadialIntersection(
        Microsoft.Xna.Framework.Vector2 center,
        Microsoft.Xna.Framework.Vector2 pointOutside,
        int radius
    )
    {
        double angle = CalculateAngle(pointOutside, center);
        Microsoft.Xna.Framework.Vector2 intersectionPoint = new(
            (float)((radius) * Math.Cos(angle) + center.X),
            (float)((radius) * Math.Sin(angle) + center.Y)
        );
        return intersectionPoint;
    }
}
