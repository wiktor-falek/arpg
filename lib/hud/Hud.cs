using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

public class Hud
{
    private List<IHudElement> _elements = [];

    public Hud()
    {
        _elements.Add(new HealthGlobe());
    }

    // HUD automatically will update upon drawing (for now)
    public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        foreach (var element in _elements)
        {
            element.Draw(spriteBatch, graphicsDevice);
        }
    }
}
