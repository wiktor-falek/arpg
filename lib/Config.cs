using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Config
{
    public int Scale { get; private set; }
    private GraphicsDeviceManager _graphics;
    private GraphicsDevice _device;
    private RenderTarget2D _renderTarget;

    public Config(
        GraphicsDeviceManager graphics,
        GraphicsDevice device,
        RenderTarget2D renderTarget
    )
    {
        _graphics = graphics;
        _device = device;
        _renderTarget = renderTarget;
    }

    public void ChangeResolutionScale(int scale)
    {
        _graphics.PreferredBackBufferWidth = 640 * scale;
        _graphics.PreferredBackBufferHeight = 360 * scale;
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
        Game1.ScaleX = (float)_device.Viewport.Width / _renderTarget.Width;
        Game1.ScaleY = (float)_device.Viewport.Height / _renderTarget.Height;
    }
}
