using arpg;
using Microsoft.Xna.Framework.Input;

public class KeyboardInputManager(Game1 game)
{
    private Game1 _game = game;
    private KeyboardState _previousKeyboardState;

    public void Update()
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Escape))
            _game.Exit();

        if (keyboardState.IsKeyDown(Keys.F1) && _previousKeyboardState.IsKeyUp(Keys.F1))
        {
            GameState.IsDebugMode = !GameState.IsDebugMode;
        }

        if (
            (keyboardState.IsKeyDown(Keys.F11) && _previousKeyboardState.IsKeyUp(Keys.F11))
            || (
                keyboardState.IsKeyDown(Keys.LeftAlt)
                && keyboardState.IsKeyDown(Keys.Enter)
                && _previousKeyboardState.IsKeyUp(Keys.Enter)
            )
        )
        {
            Game1.Config.ToggleFullScreen();
            Game1.Config.ApplyChanges();
        }

        if (keyboardState.IsKeyDown(Keys.F10) && _previousKeyboardState.IsKeyUp(Keys.F10))
        {
            int scale = (Game1.Config.Scale % 3) + 1;
            Game1.Config.ChangeResolutionScale(scale);
            Game1.Config.ApplyChanges();
        }

        _previousKeyboardState = keyboardState;
    }
}
