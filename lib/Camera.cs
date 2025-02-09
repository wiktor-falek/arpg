using System.Numerics;
using arpg;
using Microsoft.Xna.Framework;

public static class Camera
{
    public static Matrix Transform { get; private set; }
    public static Microsoft.Xna.Framework.Vector2 CameraOrigin { get; private set; }
    public static Microsoft.Xna.Framework.Vector2 CameraOffset { get; private set; }

    public static void Follow(IActor actor)
    {
        Matrix position = Matrix.CreateTranslation(
            -(int)actor.Position.X,
            -(int)actor.Position.Y,
            0
        );

        Matrix offset = Matrix.CreateTranslation(
            Game1.NativeResolution.Width / 2,
            Game1.NativeResolution.Height / 2,
            0
        );
        Transform = position * offset;

        CameraOffset = new(-Game1.NativeResolution.Width / 2, -Game1.NativeResolution.Height / 2);

        CameraOrigin = new(
            actor.Position.X - Game1.NativeResolution.Width / 2,
            actor.Position.Y - Game1.NativeResolution.Height / 2
        );
    }
}
