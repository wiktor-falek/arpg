using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PauseMenu : IOnCloseHandler
{
    public bool IsDisplayed => !GameState.IsRunning;

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsDisplayed) return;

        string text = "Game Paused";
        int textWidth = (int)Assets.Fonts.MonogramExtened.MeasureString(text).X;

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            text,
            new((Game1.NativeResolution.Width - textWidth) / 2, Game1.NativeResolution.Height / 2),
            Color.White
        );
    }

    public bool OnClose()
    {
        GameState.IsRunning = !GameState.IsRunning;
        return true;
    }
}