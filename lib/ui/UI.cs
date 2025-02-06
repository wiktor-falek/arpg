using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class UI : IOnCloseHandler
{
    private bool _isOpen = false;

    public UI()
    {
        Game1.InputManager.OnPress(RemappableGameAction.OpenInventory, Toggle);
    }

    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!_isOpen)
            return;

        // spriteBatch.Draw();
        /*
            draw a window at the right screen side, full window height
        */
    }

    public void Toggle()
    {
        _isOpen = !_isOpen;
    }

    public bool OnClose()
    {
        return false;
    }
}
