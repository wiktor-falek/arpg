using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public static class MouseManager
{
    public static Vector2 GetMousePosition()
    {
        Point position = Mouse.GetState().Position;
        return new Vector2(position.X, position.Y);
    }

    public static Vector2 GetInGameMousePosition()
    {
        Vector2 mousePosition = GetMousePosition();
        Vector2 mousePositionInGame = new Vector2(
            mousePosition.X / Game1.Config.Scale,
            mousePosition.Y / Game1.Config.Scale
        );
        return mousePositionInGame;
    }
}
