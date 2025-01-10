using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Hud
{
    private List<IHudElement> _elements = [];

    public Hud()
    {
        _elements.Add(new HealthGlobe());
        _elements.Add(new ManaGlobe());
        _elements.Add(new DebugScreen());
    }

    public void Update(GameTime gameTime)
    {
        foreach (var element in _elements)
        {
            element.Update(gameTime);
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
