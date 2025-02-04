using System;
using arpg;
using Microsoft.Xna.Framework.Input;

public class KeyboardInputManager(Game1 game)
{
    public event Action EscapePressed;
    public event Action F1Pressed;
    public event Action F10Pressed;
    public event Action F11Pressed;
    public event Action LeftAltEnterPressed;
    
    private Game1 _game = game;
    private KeyboardState _keyboardState;
    private KeyboardState _previousKeyboardState;

    public void Update()
    {
        _keyboardState = Keyboard.GetState();

        if (IsNewKeyPress(Keys.Escape))
            EscapePressed?.Invoke();

        if (IsNewKeyPress(Keys.F1))
            F1Pressed?.Invoke();

        if (IsNewKeyPress(Keys.F10))
            F10Pressed?.Invoke();

        if (IsNewKeyPress(Keys.F11))
            F11Pressed?.Invoke();

        if (_keyboardState.IsKeyDown(Keys.LeftAlt) && IsNewKeyPress(Keys.Enter))
            LeftAltEnterPressed?.Invoke();

        _previousKeyboardState = _keyboardState;
    }

    private bool IsNewKeyPress(Keys key)
    {
        return _keyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    }
}
