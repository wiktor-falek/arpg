using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public class InputManager
{
    private HashSet<Keys> _hardBoundKeys = [];
    private HashSet<Keys> _boundKeys = [];

    private KeyboardInputManager _keyboardInputManager;
    private InputMapper _inputMapper;

    public InputManager()
    {
        _keyboardInputManager = new();
        _inputMapper = new(_keyboardInputManager);

        // hardcoded keybinds
        BindKey(Keys.Escape, FixedGameAction.Close);

        // remappable keybinds
        BindKey(Keys.Q, RemappableGameAction.CastBarOne);
        BindKey(Keys.E, RemappableGameAction.CastBarTwo);
        BindKey(Keys.R, RemappableGameAction.CastBarThree);
        BindKey(Keys.F1, RemappableGameAction.DebugMenu);
        BindKey(Keys.OemTilde, RemappableGameAction.OpenInventory);
        BindKey(Keys.F10, RemappableGameAction.CycleResolution);
        BindKey(Keys.F11, RemappableGameAction.ToggleFullscreen);
        // BindKey([Keys.LeftAlt, Keys.Enter], RemappableGameAction.ToggleFullscreen);
    }

    public void Update()
    {
        _keyboardInputManager.Update(_hardBoundKeys, _boundKeys);
    }

    public void OnPress(FixedGameAction gameAction, Action handler)
    {
        _inputMapper.OnPress(gameAction, handler);
    }

    public void OnPress(RemappableGameAction gameAction, Action handler)
    {
        _inputMapper.OnPress(gameAction, handler);
    }

    public void OnRelease(FixedGameAction gameAction, Action handler)
    {
        _inputMapper.OnRelease(gameAction, handler);
    }

    public void OnRelease(RemappableGameAction gameAction, Action handler)
    {
        _inputMapper.OnRelease(gameAction, handler);
    }

    public void BindKey(Keys key, RemappableGameAction gameAction) 
    {
        _boundKeys.Add(key);
        _inputMapper.BindKey(key, gameAction);
    }

    public void UnbindKey(Keys key) 
    {
        if (_hardBoundKeys.Contains(key)) 
            throw new InvalidOperationException("Cannot unbind a fixed keybind.");
        _boundKeys.Remove(key);
        _inputMapper.UnbindKey(key);
    }

    private void BindKey(Keys key, FixedGameAction gameAction) 
    {
        _hardBoundKeys.Add(key);
        _inputMapper.BindKey(key, gameAction);
    }
}