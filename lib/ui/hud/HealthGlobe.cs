using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HealthGlobe : IHudElement
{
    public double Health { get; set; }
    public double MaxHealth { get; set; }

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
        Health = Math.Floor(GameState.Player.Stats.Health);
        MaxHealth = GameState.Player.Stats.MaxHealth;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Assets.RectangleTexture,
            Rectangle,
            null,
            new Color(150, 10, 10),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.HUD
        );

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
