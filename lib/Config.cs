using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Config
{
    public int Scale { get; private set; }
    private GraphicsDeviceManager _graphics;
    private RenderTarget2D _renderTarget;

    private float _scaleX;
    private float _scaleY;

    public Config(GraphicsDeviceManager graphics, RenderTarget2D renderTarget)
    {
        _graphics = graphics;
        _renderTarget = renderTarget;
    }

    public void ChangeResolutionScale(int scale)
    {
        _graphics.PreferredBackBufferWidth = 640 * scale;
        _graphics.PreferredBackBufferHeight = 360 * scale;
        _scaleX = (float)_graphics.GraphicsDevice.Viewport.Width / _renderTarget.Width;
        _scaleY = (float)_graphics.GraphicsDevice.Viewport.Height / _renderTarget.Height;
        Scale = scale;
    }

    public void ToggleFullScreen()
    {
        _graphics.IsFullScreen = !_graphics.IsFullScreen;
    }

    public void SetFullScreen()
    {
        _graphics.IsFullScreen = true;
    }

    public void SetMinimizedScreen()
    {
        _graphics.IsFullScreen = false;
    }

    public void ApplyChanges()
    {
        _graphics.ApplyChanges();
        Game1.ScaleX = _scaleX;
        Game1.ScaleY = _scaleY;
    }
}
