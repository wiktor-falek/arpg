using arpg;
using Microsoft.Xna.Framework;

public static class Camera
{
    public static Matrix Transform;

    public static void Follow(IActor actor)
    {
        var position = Matrix.CreateTranslation(-(int)actor.Position.X, -(int)actor.Position.Y, 0);
        var offset = Matrix.CreateTranslation(Game1.NativeResolution.Width / 2, Game1.NativeResolution.Height / 2, 0);
        Transform = position * offset;
    }
}
