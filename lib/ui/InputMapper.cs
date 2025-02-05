using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

/*
    player presses escape, KeyboardInputManager emits KeyPressed(Keys key = Escape)
    escape is not remappable, triggers FixedGameAction.Close event
    controller that subscribes to Close event triggers - things happen

    player presses Q, KeyboardInputManager emits KeyPressed(Keys key = Q)
    Q is bound to CastbarOne, triggers RemappableGameAction.CastbarOne
    some controller responsible for using spells on the cast bar maps from CastbarOne to some spell
    spell is casted 

*/

public enum FixedGameAction
{
    Close
}

public enum RemappableGameAction
{
    CastBarOne
}

public class InputMapper
{

    private KeyboardInputManager _keyboardInputManager;
    private Dictionary<FixedGameAction, Action> _fixedActionHandlers = [];
    private Dictionary<RemappableGameAction, Action> _remappableActionHandlers = [];

    public InputMapper(KeyboardInputManager keyboardInputManager)
    {
        _keyboardInputManager = keyboardInputManager;

        // initialize handlers for each FixedGameAction
        foreach (FixedGameAction action in Enum.GetValues(typeof(FixedGameAction)))
        {
            _fixedActionHandlers[action] = null;
        }

        _keyboardInputManager.KeyPressed += (Keys key) => TriggerAction(FixedGameAction.Close);
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

    private void TriggerAction(FixedGameAction gameAction)
    {
        if (_fixedActionHandlers.TryGetValue(gameAction, out var handler))
            handler?.Invoke();
    }


    public void BindKey(Keys key, RemappableGameAction gameAction)
    {

        // if (_inputMappings.ContainsKey(key))
        //     _inputMappings.Remove(key);
        // _inputMappings.Add(key, action);
    }
    /*
    keys like escape, ctrl, alt, shift can't be remapped
    
    SpacePressed triggered -> trigger mapped Action 
    */

}
