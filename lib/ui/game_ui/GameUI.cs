using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameUI
{
    private InventoryUI _inventoryUI = new();

    public GameUI()
    {
        Game1.InputManager.OnPress(RemappableGameAction.OpenInventory, ToggleInventory);
    }

    public void Update(GameTime gameTime)
    {
        _inventoryUI.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _inventoryUI.Draw(spriteBatch);
    }

    public void ToggleInventory()
    {
        if (GameState.IsRunning)
            _inventoryUI.IsOpen = !_inventoryUI.IsOpen;
    }

    public bool OnClose()
    {
        if (_inventoryUI.IsOpen)
        {
            _inventoryUI.IsOpen = false;
            return true;
        }
        return false;
    }

    public bool OnLeftClick()
    {
        Vector2 mousePosition = Game1.InputManager.MousePosition;

        if (_inventoryUI.OnClick(mousePosition))
            return true;

        return false;
    }
}
