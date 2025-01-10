using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public static class MouseManager
{
    public static Vector2 GetMousePosition()
    {
        MouseState mouseState = Mouse.GetState();
        Vector2 mousePositionInGame = new Vector2(
            mouseState.Position.X / Game1.ScaleX,
            mouseState.Position.Y / Game1.ScaleY
        );
        mousePositionInGame = Vector2.Transform(
            mousePositionInGame,
            Matrix.Invert(Camera.Transform)
        );

        return mousePositionInGame;
    }
}
