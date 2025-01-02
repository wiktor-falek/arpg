using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HealthGlobe : IHudElement
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; } = new Vector2(0, 405);
    public Vector2 Size { get; set; } = new Vector2(75, 75);
    public Rectangle Rectangle
    {
        get => new((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
    }

    public uint CurrentHealth { get; set; }
    public uint MaxHealth { get; set; }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        var globeTexture = new Texture2D(graphicsDevice, 1, 1);
        globeTexture.SetData([Color.Red]);
        spriteBatch.Draw(globeTexture, Rectangle, Color.Red);

        float scale = 1.5f;
        string healthGlobeValuesText = $"{CurrentHealth} / {MaxHealth}";
        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            healthGlobeValuesText,
            new Vector2(Position.X, Position.Y - 12 * scale),
            Color.Red,
            0.0f,
            Vector2.Zero,
            scale,
            SpriteEffects.None,
            0.0f
        );
    }

    public void Update() { }
};
