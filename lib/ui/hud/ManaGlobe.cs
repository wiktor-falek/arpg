using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ManaGlobe : IHudElement
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public Vector2 Size { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public ManaGlobe()
    {
        Size = new Vector2(55, 55);
        Position = new Vector2(640 - Size.X, 360 - Size.Y);
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        var globeTexture = new Texture2D(graphicsDevice, 1, 1);
        globeTexture.SetData([new Color(10, 10, 140)]);
        spriteBatch.Draw(globeTexture, Rectangle, Color.White);

        string manaGlobeValuesText = $"{Health}/{MaxHealth}";
        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            manaGlobeValuesText,
            new Vector2(Position.X, Position.Y - 12),
            Color.White,
            0.0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.Text
        );
    }

    public void Update()
    {
        Health = GameState.Player.Health;
        MaxHealth = GameState.Player.MaxHealth;
    }
};
