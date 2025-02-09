using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class InventoryUI
{
    public bool IsOpen = false;
    public Rectangle Bounds;

    private Player _player;

    public InventoryUI(Player player)
    {
        _player = player;
        int windowWidth = 220;
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
            Layer.UIWindow
        );

        const int SQUARE_SIZE = 16;
        const int BORDER_SIZE = 1;
        int inventoryHeight = _player.Inventory.Height * (SQUARE_SIZE + BORDER_SIZE) - 1;
        int inventoryWidth = _player.Inventory.Width * (SQUARE_SIZE + BORDER_SIZE) - 1;

        for (int i = 0; i < _player.Inventory.Width; i++)
        {
            for (int j = 0; j < _player.Inventory.Height; j++)
            {
                int x = Bounds.Left + i * (SQUARE_SIZE + BORDER_SIZE);
                int y = Bounds.Bottom - inventoryHeight + j * (SQUARE_SIZE + BORDER_SIZE);

                spriteBatch.Draw(
                    Assets.RectangleTexture,
                    new(x, y, SQUARE_SIZE, SQUARE_SIZE),
                    null,
                    Color.Gray,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    Layer.UIWindowElement
                );
            }
        }
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
