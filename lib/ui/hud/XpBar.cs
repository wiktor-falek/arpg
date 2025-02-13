using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class XpBar : IHudElement
{
    public Vector2 Position { get; set; }
    public int Level { get; private set; }
    public int CurrentXP { get; private set; }
    public int RequiredXP { get; private set; }

    public XpBar()
    {
        Position = new Vector2(
            Game1.NativeResolution.Width / 2,
            Game1.NativeResolution.Height - 12
        );
    }

    public void Update(GameTime gameTime)
    {
        PlayerStats playerStats = (PlayerStats)GameState.Player.Stats;
        Level = playerStats.Level.Current;
        CurrentXP = playerStats.Level.CurrentXP;
        RequiredXP = playerStats.Level.RequiredXP;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        string xpBarText = $"Lv{Level} {CurrentXP}/{RequiredXP}";
        int xpBarTextWidth = (int)Assets.Fonts.MonogramExtened.MeasureString(xpBarText).X;
        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            xpBarText,
            new Vector2(Position.X - xpBarTextWidth / 2, Position.Y),
            Color.Yellow,
            0.0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.Text
        );
    }
};
