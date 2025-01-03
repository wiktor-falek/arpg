using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

// string playerHealthText = $"Player HP: {Player.Health}";
// string playerPositionText = $"Player X: {Player.Position.X} Y: {Player.Position.Y}";

// float textScale = 1.0f;
// float layerdepth = 1.0f;
// float rotation = 0.0f;
// int fontHeight = 16;

// _spriteBatch.DrawString(
//     Assets.Fonts.MonogramExtened,
//     playerHealthText,
//     new Vector2(0, fontHeight * 0),
//     Color.Black,
//     rotation,
//     Vector2.Zero,
//     textScale,
//     SpriteEffects.None,
//     layerdepth
// );

// _spriteBatch.DrawString(
//     Assets.Fonts.MonogramExtened,
//     playerPositionText,
//     new Vector2(0, fontHeight * 1),
//     Color.Black,
//     rotation,
//     Vector2.Zero,
//     textScale,
//     SpriteEffects.None,
//     layerdepth
// );


public class Hud
{
    private List<IHudElement> _elements = [];

    public Hud()
    {
        _elements.Add(new HealthGlobe());
    }

    public void Update()
    {
        foreach (var element in _elements)
        {
            element.Update();
        }
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        foreach (var element in _elements)
        {
            element.Draw(spriteBatch, graphicsDevice);
        }
    }
}
