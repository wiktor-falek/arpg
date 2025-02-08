using System;
using System.Collections.Generic;
using arpg;

public class GameInputController
{
    private readonly List<Func<bool>> _escapeHandlers = [];
    private readonly List<Func<bool>> _leftClickHandlers = [];

    public GameInputController()
    {
        Game1.InputManager.OnPress(FixedGameAction.Close, OnClose);
        Game1.InputManager.OnPress(FixedGameAction.LeftClick, OnLeftClick);
        Game1.InputManager.OnPress(RemappableGameAction.DebugMenu, ToggleDebugMode);
        Game1.InputManager.OnPress(RemappableGameAction.CycleResolution, CycleResolution);
        Game1.InputManager.OnPress(RemappableGameAction.ToggleFullscreen, ToggleFullscreen);
    }

    public void RegisterOnClose(Func<bool> handler)
    {
        _escapeHandlers.Add(handler);
    }

    public void RegisterOnLeftClick(Func<bool> handler)
    {
        _leftClickHandlers.Add(handler);
    }

    private void OnClose()
    {
        HandleEventPropagation(_escapeHandlers);
    }

    private void OnLeftClick()
    {
        HandleEventPropagation(_leftClickHandlers);
    }

    private void HandleEventPropagation(List<Func<bool>> handlers)
    {
        foreach (Func<bool> handler in handlers)
        {
            bool propagationStopped = handler();
            if (propagationStopped) break;
        }
    }

    // TODO: move these somewhere else?
    private void ToggleDebugMode()
    {
        GameState.IsDebugMode = !GameState.IsDebugMode;
    }

    private void CycleResolution()
    {
        int scale = (Game1.Config.Scale % 3) + 1;
        Game1.Config.ChangeResolutionScale(scale);
        Game1.Config.ApplyChanges();
    }

    private void ToggleFullscreen()
    {
        Game1.Config.ToggleFullScreen();
        Game1.Config.ApplyChanges();
    }
}
