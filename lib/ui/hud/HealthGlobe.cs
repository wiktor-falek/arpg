using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HealthGlobe : IHudElement
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public Vector2 Size { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public HealthGlobe()
    {
        Size = new Vector2(55, 55);
        Position = new Vector2(0, Game1.NativeResolution.Height - Size.Y);
    }

    public void Update(GameTime gameTime)
    {
        Health = GameState.Player.Stats.Health;
        MaxHealth = GameState.Player.Stats.MaxHealth;
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        var globeTexture = new Texture2D(graphicsDevice, 1, 1);
        globeTexture.SetData([new Color(150, 10, 10)]);
        spriteBatch.Draw(globeTexture, Rectangle, Color.White);

        string healthGlobeValuesText = $"{Health}/{MaxHealth}";
        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            healthGlobeValuesText,
            new Vector2(Position.X, Position.Y - 12),
            Color.White,
            0.0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.Text
        );
    }
};
