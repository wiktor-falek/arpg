using Microsoft.Xna.Framework;

public class Config
{
    private GraphicsDeviceManager _graphics;

    public Config(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;
    }

    public void ChangeResolutionScale(int scale)
    {
        _graphics.PreferredBackBufferWidth = 640 * scale;
        _graphics.PreferredBackBufferHeight = 360 * scale;
    }

    public void SetFullScreen()
    {
        _graphics.IsFullScreen = true;
    }

    public void ApplyChanges()
    {
        _graphics.ApplyChanges();
    }
}
