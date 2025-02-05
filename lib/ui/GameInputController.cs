using System.Collections.Generic;
using arpg;

interface IOnCloseHandler
{
    /// <summary>
    /// Handler method that will be called if previous handlers in the hierarchy didn't break the propagation.
    /// </summary>
    /// <returns>Whether to stop event propagation or not.</returns>
    bool OnClose();
}

public class GameInputController
{
    private readonly List<IOnCloseHandler> _escapeHandlers = [];

    public GameInputController(UI ui, PauseMenu pauseMenu)
    {
        Game1.InputMapper.OnPress(FixedGameAction.Close, OnClose);
        Game1.InputMapper.OnPress(RemappableGameAction.DebugMenu, ToggleDebugMode);
        Game1.InputMapper.OnPress(RemappableGameAction.CycleResolution, CycleResolution);
        Game1.InputMapper.OnPress(RemappableGameAction.ToggleFullscreen, ToggleFullscreen);

        _escapeHandlers.Add(ui);
        _escapeHandlers.Add(pauseMenu);
    }

    private void OnClose()
    {
        foreach (IOnCloseHandler handler in _escapeHandlers)
        {
            bool propagationStopped = handler.OnClose();
            if (propagationStopped)
                break;
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
