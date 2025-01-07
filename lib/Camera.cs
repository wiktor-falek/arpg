using Microsoft.Xna.Framework;

public class Camera
{
    public Matrix Transform;

    public void Follow(Player player)
    {
        var position = Matrix.CreateTranslation(
            -(int)player.Position.X,
            -(int)player.Position.Y,
            0
        );
        var offset = Matrix.CreateTranslation(640 / 2, 360 / 2, 0);
        Transform = position * offset;
    }
}
