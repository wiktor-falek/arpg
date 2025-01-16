using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DebugScreen : IHudElement
{
    public void Update(GameTime gameTime)
    {
        // FramerateCounter.Update(gameTime.ElapsedGameTime.TotalSeconds);
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        if (!GameState.IsDebugMode)
            return;

        // string playerPositionXText = $"X:{GameState.Player.Position.X:F2}";
        // string playerPositionYText = $"Y:{GameState.Player.Position.Y:F2}";
        string playerPositionXText = $"X:{Game1.Config.ScaleX:F2}";
        string playerPositionYText = $"Y:{Game1.Config.ScaleY:F2}";
        string resolutionText = $"Resolution:{640 * Game1.Config.Scale}x{360 * Game1.Config.Scale}";
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

        // spriteBatch.DrawString(
        //     Assets.Fonts.MonogramExtened,
        //     framerateText,
        //     new Vector2(0, 30),
        //     Color.White,
        //     0f,
        //     Vector2.Zero,
        //     1f,
        //     SpriteEffects.None,
        //     Layer.Text
        // );
    }
}
