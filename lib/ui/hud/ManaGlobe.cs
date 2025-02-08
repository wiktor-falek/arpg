using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ManaGlobe : IHudElement
{
    public double Mana { get; set; } = 100;
    public double MaxMana { get; set; } = 100;

    public Vector2 Size { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public ManaGlobe()
    {
        Size = new Vector2(55, 55);
        Position = new Vector2(Game1.NativeResolution.Width - Size.X, Game1.NativeResolution.Height - Size.Y);
    }

    public void Update(GameTime gameTime)
    {
        Mana = Math.Floor(GameState.Player.Stats.Mana);
        MaxMana = Math.Floor(GameState.Player.Stats.MaxMana);
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        var globeTexture = new Texture2D(graphicsDevice, 1, 1);
        globeTexture.SetData([new Color(10, 10, 140)]);
            spriteBatch.Draw(
            globeTexture,
            Rectangle,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.HUD
        );

        string manaGlobeValuesText = $"{Mana}/{MaxMana}";
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
};
