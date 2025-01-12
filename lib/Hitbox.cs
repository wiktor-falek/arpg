using System;
using Microsoft.Xna.Framework;

public interface IHitbox
{
    bool Intersects(IHitbox other);
}

// TODO: visitor pattern
public class RectangleHitbox(int x, int y, int width, int height) : IHitbox
{
    public Rectangle Bounds { get; set; } = new(x, y, width, height);

    public bool Intersects(IHitbox other)
    {
        return other switch
        {
            RectangleHitbox rectangleHitbox => Intersects(rectangleHitbox),
            CircleHitbox circleHitbox => Intersects(circleHitbox),
            _ => throw new NotImplementedException(),
        };
    }

    public bool Intersects(RectangleHitbox rectangleHitbox)
    {
        return Bounds.Intersects(rectangleHitbox.Bounds);
    }

    public bool Intersects(CircleHitbox circleHitbox)
    {
        throw new NotImplementedException();
    }
}

public class CircleHitbox(Vector2 center, double radius) : IHitbox
{
    public Vector2 Center = center;
    public double Radius = radius;

    public bool Intersects(IHitbox other)
    {
        return other switch
        {
            RectangleHitbox rectangleHitbox => Intersects(rectangleHitbox),
            CircleHitbox circleHitbox => Intersects(circleHitbox),
            _ => throw new NotImplementedException(),
        };
    }

    public bool Intersects(CircleHitbox circle)
    {
        double x1 = Center.X;
        double y1 = Center.Y;
        double x2 = circle.Center.X;
        double y2 = circle.Center.Y;
        double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        return distance <= Radius + circle.Radius;
    }

    public bool Intersects(Rectangle rectangle)
    {
        throw new NotImplementedException();
        // case 1: circle center is in rectangle
        // point in polygon - 0 ≤ AP·AB ≤ AB·AB and 0 ≤ AP·AD ≤ AD·AD

        // case 2: circle intersects polygon vertices
        // foot of the perpendicular from Center to the line is close enough and between the endpoints,
        // and check the endpoints otherwise

        // return false;
    }
}
