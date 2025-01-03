using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

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
