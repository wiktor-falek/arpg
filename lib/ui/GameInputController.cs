using System.Collections.Generic;
using arpg;

interface IEscapeHandler
{
    /// <summary>
    /// Handler method that will be called if previous handlers in the hierarchy didn't break the propagation.
    /// </summary>
    /// <returns>Whether to stop event propagation or not.</returns>
    bool OnEscape();
}

public class GameInputController
{
    private readonly List<IEscapeHandler> _escapeHandlers = [];
    
    public GameInputController(
        KeyboardInputManager keyboardInputManager,
        UI ui,
        PauseMenu pauseMenu
    )
    {
        // TODO: input mapper
        keyboardInputManager.EscapePressed += OnEscape;
        keyboardInputManager.F1Pressed += ToggleDebugMode;
        keyboardInputManager.F10Pressed += CycleResolution;
        keyboardInputManager.F11Pressed += ToggleFullscreen;
        keyboardInputManager.LeftAltEnterPressed += ToggleFullscreen;

        _escapeHandlers.Add(ui);
        _escapeHandlers.Add(pauseMenu);
    }

    private void OnEscape()
    {
        foreach (IEscapeHandler handler in _escapeHandlers)
        {
            bool propagationStopped = handler.OnEscape();
            if (propagationStopped) break;
        }
    }

    // TODO: move out?
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
