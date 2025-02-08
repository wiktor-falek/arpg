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

    public static Vector2 GetInGameMousePositionRelativeToPlayer()
    {
        Vector2 inGameMousePosition = GetInGameMousePosition();
        Vector2 inGameMousePositionRelativeToPlayer = Vector2.Transform(
            inGameMousePosition,
            Matrix.Invert(Camera.Transform)
        );
        return inGameMousePositionRelativeToPlayer;
    }
}
