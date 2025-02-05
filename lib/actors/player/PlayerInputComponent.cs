using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class PlayerInputComponent
{
    private bool isMoving = false;
    private double _angle = 0d;
    private Vector2 _moveDestination;

    public PlayerInputComponent() { }

    public void Update(Player player, GameTime gameTime)
    {
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
            player.TransitionState(ActorState.Walking);

            double angleInDegrees = MathHelper.ToDegrees((float)_angle);
            bool isFacingRight = angleInDegrees >= -90 && angleInDegrees <= 90;
            player.Facing = isFacingRight ? ActorFacing.Right : ActorFacing.Left;
            isMoving = true;
        }

        if (!isMoving)
            return;

        float distanceToDestination = Vector2.Distance(player.Position, _moveDestination);
        if (distanceToDestination > 1f)
        {
            double elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
            float x =
                player.Position.X + (float)(player.Stats.Speed * elapsedTime * Math.Cos(_angle));
            float y =
                player.Position.Y + (float)(player.Stats.Speed * elapsedTime * Math.Sin(_angle));
            player.Position = new(x, y);
        }
        else
        {
            isMoving = false;
            player.Position = _moveDestination;
            player.TransitionState(ActorState.Idling);
        }
    }
}
