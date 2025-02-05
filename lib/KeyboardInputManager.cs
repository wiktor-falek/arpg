using System;
using arpg;
using Microsoft.Xna.Framework.Input;

public class KeyboardInputManager(Game1 game)
{
    public event Action<Keys> KeyPressed;
    public event Action<Keys> KeyReleased;

    private Game1 _game = game;
    private KeyboardState _keyboardState;
    private KeyboardState _previousKeyboardState;

    public void Update()
    {
        _keyboardState = Keyboard.GetState();

        foreach (Keys key in Enum.GetValues(typeof(Keys)))
        {
            if (IsNewKeyPress(key))
                KeyPressed?.Invoke(key);

            if (IsKeyReleased(key))
                KeyReleased?.Invoke(key);
        }

        // if (IsNewKeyPress(Keys.Escape))
        //     EscapePressed?.Invoke();

        // if (IsNewKeyPress(Keys.Space))
        //     SpacePressed?.Invoke();

        // if (IsNewKeyPress(Keys.F1))
        //     F1Pressed?.Invoke();

        // if (IsNewKeyPress(Keys.F10))
        //     F10Pressed?.Invoke();

        // if (IsNewKeyPress(Keys.F11))
        //     F11Pressed?.Invoke();

        // if (_keyboardState.IsKeyDown(Keys.LeftAlt) && IsNewKeyPress(Keys.Enter))
        //     LeftAltEnterPressed?.Invoke();

        _previousKeyboardState = _keyboardState;
    }

    private bool IsNewKeyPress(Keys key)
    {
        return _keyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    }

    private bool IsKeyReleased(Keys key)
    {
        return _keyboardState.IsKeyUp(key) && _previousKeyboardState.IsKeyDown(key);
    }
}
