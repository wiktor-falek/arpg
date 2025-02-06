using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public enum FixedGameAction
{
    // LeftClick,
    // RightClick,
    Close,
}

public enum RemappableGameAction
{
    CastBarOne,
    CastBarTwo,
    CastBarThree,
    OpenInventory,
    DebugMenu,
    CycleResolution,
    ToggleFullscreen,
}

public class InputMapper
{
    // MouseInputManager?
    private KeyboardInputManager _keyboardInputManager;
    private Dictionary<Keys, FixedGameAction> _fixedKeybinds = [];
    private Dictionary<FixedGameAction, Action> _fixedActionHandlers = [];
    private Dictionary<FixedGameAction, Action> _fixedReleaseActionHandlers = [];
    private Dictionary<Keys, RemappableGameAction> _keybinds = [];
    private Dictionary<RemappableGameAction, Action> _remappableActionHandlers = [];
    private Dictionary<RemappableGameAction, Action> _remappableReleaseActionHandlers = [];

    public InputMapper(KeyboardInputManager keyboardInputManager)
    {
        _keyboardInputManager = keyboardInputManager;

        foreach (FixedGameAction action in Enum.GetValues(typeof(FixedGameAction)))
        {
            _fixedActionHandlers[action] = null;
            _fixedReleaseActionHandlers[action] = null;
        }

        foreach (RemappableGameAction action in Enum.GetValues(typeof(RemappableGameAction)))
        {
            _remappableActionHandlers[action] = null;
            _remappableReleaseActionHandlers[action] = null;
        }

        _keyboardInputManager.KeyPressed += TriggerAction;
        // TODO: KeyReleased
    }

    public void OnPress(FixedGameAction gameAction, Action handler)
    {
        _fixedActionHandlers[gameAction] += handler;
    }

    public void OnPress(RemappableGameAction gameAction, Action handler)
    {
        _remappableActionHandlers[gameAction] += handler;
    }

    public void OnRelease(FixedGameAction gameAction, Action handler)
    {
        _fixedReleaseActionHandlers[gameAction] += handler;
    }

    public void OnRelease(RemappableGameAction gameAction, Action handler)
    {
        _remappableReleaseActionHandlers[gameAction] += handler;
    }

    // TODO: unsubscribing

    public void BindKey(Keys key, RemappableGameAction gameAction)
    {
        _keybinds.Add(key, gameAction);
    }

    public void BindKey(Keys key, FixedGameAction gameAction)
    {
        _fixedKeybinds.Add(key, gameAction);
    }

    public void UnbindKey(Keys key)
    {
        if (_keybinds.TryGetValue(key, out var _))
            _keybinds.Remove(key);
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
