using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Config(
    GraphicsDeviceManager graphics,
    GraphicsDevice device,
    RenderTarget2D renderTarget
)
{
    public int Scale { get; private set; }
    public float ScaleX { get; private set; }
    public float ScaleY { get; private set; }
    private GraphicsDeviceManager _graphics = graphics;
    private GraphicsDevice _device = device;
    private RenderTarget2D _renderTarget = renderTarget;

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
        ScaleX = (float)_device.Viewport.Width / _renderTarget.Width;
        ScaleY = (float)_device.Viewport.Height / _renderTarget.Height;
    }
}
