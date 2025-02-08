using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class InventoryUI
{
    public bool IsOpen = false;
    public Rectangle Bounds;

    public InventoryUI()
    {
        int windowWidth = 260;
        int screenWidth = Game1.NativeResolution.Width;
        int screenHeight = Game1.NativeResolution.Height;
        Bounds = new(screenWidth - windowWidth, 0, windowWidth, screenHeight);
    }

    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsOpen)
            return;

        spriteBatch.Draw(
            Assets.RectangleTexture,
            Bounds,
            null,
            Color.Black,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.UI
        );
    }

    public bool OnClick(Vector2 mousePosition)
    {
        if (!IsOpen)
            return false;

        bool cursorWithinBounds =
            mousePosition.X > Bounds.Left
            && mousePosition.X < Bounds.Right
            && mousePosition.Y > Bounds.Top
            && mousePosition.Y < Bounds.Bottom;

        if (!cursorWithinBounds)
            return false;

        // handle click
        return true;
    }
}
