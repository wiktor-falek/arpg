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
        // Game1.InputMapper.Subscribe(
        //     RemappableGameAction.CastBarOne,
        //     () => player.Skills.Fireball.Cast(_angle)
        // );
        Game1.InputMapper.Subscribe(FixedGameAction.Close, OnClose);
        // inputMapper.F1Pressed += ToggleDebugMode;
        // inputMapper.F10Pressed += CycleResolution;
        // inputMapper.F11Pressed += ToggleFullscreen;
        // inputMapper.LeftAltEnterPressed += ToggleFullscreen;

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
