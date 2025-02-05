using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public enum FixedGameAction
{
    Close,
}

public enum RemappableGameAction
{
    CastBarOne,
}

public class InputMapper
{
    private KeyboardInputManager _keyboardInputManager;
    private Dictionary<Keys, RemappableGameAction> _keybinds = [];
    private Dictionary<RemappableGameAction, Action> _remappableActionHandlers = [];
    private Dictionary<Keys, FixedGameAction> _fixedKeybinds = [];
    private Dictionary<FixedGameAction, Action> _fixedActionHandlers = [];

    public InputMapper(KeyboardInputManager keyboardInputManager)
    {
        _keyboardInputManager = keyboardInputManager;

        // initialize handlers for each FixedGameAction
        foreach (FixedGameAction action in Enum.GetValues(typeof(FixedGameAction)))
        {
            _fixedActionHandlers[action] = null;
        }

        foreach (RemappableGameAction action in Enum.GetValues(typeof(RemappableGameAction)))
        {
            _remappableActionHandlers[action] = null;
        }

        _keyboardInputManager.KeyPressed += TriggerAction;

        // hardcoded keybinds
        BindKey(Keys.Escape, FixedGameAction.Close);

        // TODO: define elsewhere? load from config file?
        BindKey(Keys.Space, RemappableGameAction.CastBarOne);
    }

    public void Subscribe(FixedGameAction gameAction, Action handler)
    {
        _fixedActionHandlers[gameAction] += handler;
    }

    public void Subscribe(RemappableGameAction gameAction, Action handler)
    {
        _remappableActionHandlers[gameAction] += handler;
    }

    // TODO: unsubscribing

    public void BindKey(Keys key, RemappableGameAction gameAction)
    {
        if (_keybinds.TryGetValue(key, out var _))
            _keybinds.Remove(key);
        _keybinds.Add(key, gameAction);
    }

    private void BindKey(Keys key, FixedGameAction gameAction)
    {
        if (_fixedKeybinds.TryGetValue(key, out var _))
            _fixedKeybinds.Remove(key);
        _fixedKeybinds.Add(key, gameAction);
    }

    private FixedGameAction? GetFixedKeybindAction(Keys key)
    {
        if (!_fixedKeybinds.TryGetValue(key, out var fixedGameAction))
            return null;
        return fixedGameAction;
    }

    private RemappableGameAction? GetRemappableKeybindAction(Keys key)
    {
        if (!_keybinds.TryGetValue(key, out var remappableGameAction))
            return null;
        return remappableGameAction;
    }

    private void TriggerAction(Keys key)
    {
        FixedGameAction? fixedGameAction = GetFixedKeybindAction(key);
        if (fixedGameAction is not null)
        {
            _fixedActionHandlers[fixedGameAction.Value]?.Invoke();
            return;
        }

        RemappableGameAction? remappableGameAction = GetRemappableKeybindAction(key);
        if (remappableGameAction is not null)
        {
            _remappableActionHandlers[remappableGameAction.Value]?.Invoke();
            return;
        }
    }
}
