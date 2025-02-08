using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UI
{
    private bool _inventoryIsOpen = false;

    public UI()
    {
        Game1.InputManager.OnPress(RemappableGameAction.OpenInventory, Toggle);
    }

    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!_inventoryIsOpen)
            return;

        // inventory (later on moved to a class)

        int windowWidth = 260;
        int screenWidth = Game1.NativeResolution.Width;
        int screenHeight = Game1.NativeResolution.Height;

        Rectangle destination = new(screenWidth - windowWidth, 0, windowWidth, screenHeight);

        spriteBatch.Draw(
            Assets.RectangleTexture,
            destination,
            null,
            Color.Black,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.UI
        );
    }

    public void Toggle()
    {
        if (GameState.IsRunning)
            _inventoryIsOpen = !_inventoryIsOpen;
    }

    public bool OnClose()
    {
        if (_inventoryIsOpen)
        {
            _inventoryIsOpen = false;
            return true;
        }
        return false;
    }

    public bool OnLeftClick()
    {
        if (_inventoryIsOpen)
        {
            bool cursorWithinInventoryBounds = true;
            if (cursorWithinInventoryBounds) 
            {
                // inventory click logic
                return true;
            }
        }
        return false;
    }
}
