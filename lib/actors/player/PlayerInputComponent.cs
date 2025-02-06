using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class PlayerInputComponent
{
    private bool isMoving = false;
    private Vector2 _mousePosition;
    private double _mouseAngle = 0d;
    private Vector2 _destination;
    private double _destinationAngle = 0d;

    public PlayerInputComponent(Player player)
    {
        // TODO: repeat cast, only one key at a time
        Game1.InputManager.OnPress(
            RemappableGameAction.CastBarOne,
            () => player.Skills.Fireball.Cast(_mouseAngle)
        );
        Game1.InputManager.OnPress(
            RemappableGameAction.CastBarTwo,
            () => player.Skills.FrozenOrb.Cast(_mouseAngle)
        );
        Game1.InputManager.OnPress(
            RemappableGameAction.CastBarThree,
            () => player.Skills.HolyFire.Cast()
        );
    }

    public void Update(Player player, GameTime gameTime)
    {
        _mousePosition = MouseManager.GetMousePosition();
        _mouseAngle = CalculateAngle(player.Position, _mousePosition);

        MouseState mouseState = Mouse.GetState();
        bool mousePressed = mouseState.LeftButton == ButtonState.Pressed;
        if (mousePressed)
        {
            _destination = _mousePosition;
            _destinationAngle = _mouseAngle;
            double angleInDegrees = MathHelper.ToDegrees((float)_destinationAngle);
            bool isFacingRight = angleInDegrees >= -90 && angleInDegrees <= 90;
            player.Facing = isFacingRight ? ActorFacing.Right : ActorFacing.Left;
            isMoving = true;
            player.TransitionState(ActorState.Walking);
        }

        if (!isMoving)
            return;

        float distanceToDestination = Vector2.Distance(player.Position, _destination);
        if (distanceToDestination > 1f)
        {
            double elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
            float x =
                player.Position.X
                + (float)(player.Stats.Speed * elapsedTime * Math.Cos(_destinationAngle));
            float y =
                player.Position.Y
                + (float)(player.Stats.Speed * elapsedTime * Math.Sin(_destinationAngle));
            player.Position = new(x, y);
        }
        else
        {
            player.Position = _destination;
            isMoving = false;
            player.TransitionState(ActorState.Idling);
        }
    }

    private double CalculateAngle(Vector2 first, Vector2 second)
    {
        float x1 = first.X;
        float y1 = first.Y;
        float x2 = second.X;
        float y2 = second.Y;
        float deltaX = x2 - x1;
        float deltaY = y2 - y1;
        double angle = Math.Atan2(deltaY, deltaX);
        return angle;
    }
}
