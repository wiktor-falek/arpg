using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DebugScreen : IHudElement
{
    public DebugScreen() { }

    public void Update(GameTime gameTime)
    {
        // FramerateCounter.Update(gameTime.ElapsedGameTime.TotalSeconds);
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        if (!GameState.IsDebugMode)
            return;

        string playerPositionXText = $"X:{GameState.Player.Position.X:F2}";
        string playerPositionYText = $"Y:{GameState.Player.Position.Y:F2}";
        string resolutionText =
            $"Resolution:{Game1.NativeResolution.Width * Game1.Config.Scale}"
            + $"x{Game1.NativeResolution.Height * Game1.Config.Scale}";
        string mousePositionText =
            $"Cursor=({MouseManager.GetInGameMousePosition().X}, {MouseManager.GetInGameMousePosition().Y})";

        Vector2 inGameMousePosition = MouseManager.GetInGameMousePosition();
        Vector2 cursorCoordinate = Camera.CameraOrigin + inGameMousePosition;
        string cursorCoordinateText =
            $"CursorCoordinate=({cursorCoordinate.X}, {cursorCoordinate.Y})";
        // string framerateText = $"FPS:{(int)FramerateCounter.Framerate}";

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            playerPositionXText,
            new Vector2(0, 0),
            Color.White,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.Text
        );

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            playerPositionYText,
            new Vector2(0, 10),
            Color.White,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.Text
        );

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            resolutionText,
            new Vector2(0, 20),
            Color.White,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.Text
        );

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            mousePositionText,
            new Vector2(0, 30),
            Color.White,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.Text
        );

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            cursorCoordinateText,
            new Vector2(0, 40),
            Color.White,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.Text
        );
    }
}
