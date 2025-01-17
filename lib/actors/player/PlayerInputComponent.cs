using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class PlayerInputComponent
{
    private KeyboardState _previousKeyboardState;

    public void Update(Player player, GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        Vector2 mousePosition = MouseManager.GetMousePosition();

        float x1 = player.Position.X;
        float y1 = player.Position.Y;
        float x2 = mousePosition.X;
        float y2 = mousePosition.Y;
        float deltaX = x2 - x1;
        float deltaY = y2 - y1;
        double angle = Math.Atan2(deltaY, deltaX);

        if (keyboardState.IsKeyDown(Keys.Space))
        {
            player.Skills.Fireball.Cast(angle);
        }

        if (keyboardState.IsKeyDown(Keys.Q))
        {
            player.Skills.FrozenOrb.Cast(angle);
        }

        if (keyboardState.IsKeyDown(Keys.R) && _previousKeyboardState.IsKeyUp(Keys.R))
        {
            player.Skills.HolyFire.Cast();
        }

        Vector2 movementDirection = Vector2.Zero;
        if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
        {
            movementDirection.Y -= 1;
        }

        if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
        {
            movementDirection.Y += 1;
        }

        if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
        {
            movementDirection.X -= 1;
        }

        if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
        {
            movementDirection.X += 1;
        }

        if (movementDirection.X == 1)
        {
            player.Facing = ActorFacing.Right;
        }
        else if (movementDirection.X == -1)
        {
            player.Facing = ActorFacing.Left;
        }

        if (movementDirection.Length() > 0)
        {
            movementDirection.Normalize();
            player.TransitionState(ActorState.Walking);
        }
        else
        {
            player.TransitionState(ActorState.Idling);
        }
        float updatedSpeed = player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        player.Position += movementDirection * updatedSpeed;

        _previousKeyboardState = keyboardState;
    }
}
