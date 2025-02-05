using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class PlayerInputComponent
{
    private double _angle = 0d;
    private Vector2 _moveDestination;

    // private Vector2 _movementDirection = Vector2.Zero;

    public PlayerInputComponent(Player player) { }

    public void Update(Player player, GameTime gameTime)
    {
        // KeyboardState keyboardState = Keyboard.GetState();
        MouseState mouseState = Mouse.GetState();
        bool mousePressed = mouseState.LeftButton == ButtonState.Pressed;

        if (mousePressed)
        {
            Vector2 mousePosition = MouseManager.GetMousePosition();
            _moveDestination = mousePosition;
            float x1 = player.Position.X;
            float y1 = player.Position.Y;
            float x2 = _moveDestination.X;
            float y2 = _moveDestination.Y;
            float deltaX = x2 - x1;
            float deltaY = y2 - y1;
            _angle = Math.Atan2(deltaY, deltaX);
        }

        // if hasnt reached the destination
        double elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
        float x = player.Position.X + (float)(player.Stats.Speed * elapsedTime * Math.Cos(_angle));
        float y = player.Position.Y + (float)(player.Stats.Speed * elapsedTime * Math.Sin(_angle));
        player.Position = new(x, y);

        // if (_movementDirection.X == 1)
        // {
        //     player.Facing = ActorFacing.Right;
        // }
        // else if (_movementDirection.X == -1)
        // {
        //     player.Facing = ActorFacing.Left;
        // }

        // if (_movementDirection.Length() > 0)
        // {
        //     _movementDirection.Normalize();
        //     player.TransitionState(ActorState.Walking);
        // }
        // else
        // {
        //     player.TransitionState(ActorState.Idling);
        // }

        float updatedSpeed = (float)(player.Stats.Speed * gameTime.ElapsedGameTime.TotalSeconds);
        // player.Position += _movementDirection * updatedSpeed;

        // _movementDirection = Vector2.Zero;
    }
}
