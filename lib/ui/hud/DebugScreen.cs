using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DebugScreen
{
    public void Draw(SpriteBatch spriteBatch)
    {
        if (!GameState.IsDebugMode)
            return;

        string playerPositionXText = $"X: {GameState.Player.Position.X:F2}";
        string playerPositionYText = $"Y: {GameState.Player.Position.Y:F2}";

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
    }
}
