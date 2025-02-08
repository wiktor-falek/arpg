using System;
using arpg;
using Microsoft.Xna.Framework;

public class PlayerInputComponent
{
    private Player _player;
    private bool isMoving = false;
    private Vector2 _mousePosition;
    private double _mouseAngle = 0d;
    private Vector2 _destination;
    private double _destinationAngle = 0d;

    public PlayerInputComponent(Player player)
    {
        _player = player;
        // TODO: don't cast if game paused
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

    public void Update(GameTime gameTime)
    {
        // TODO: update destination while click is held, stop on release
        if (!isMoving)
            return;

        float distanceToDestination = Vector2.Distance(_player.Position, _destination);
        if (distanceToDestination > 1f)
        {
            double elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
            float x =
                _player.Position.X
                + (float)(_player.Stats.Speed * elapsedTime * Math.Cos(_destinationAngle));
            float y =
                _player.Position.Y
                + (float)(_player.Stats.Speed * elapsedTime * Math.Sin(_destinationAngle));
            _player.Position = new(x, y);
        }
        else
        {
            _player.Position = _destination;
            isMoving = false;
            _player.TransitionState(ActorState.Idling);
        }
    }

    public bool OnLeftClick()
    {
        if (!GameState.IsRunning)
            return false;
        _mousePosition = MouseManager.GetMousePosition();
        _mouseAngle = CalculateAngle(_player.Position, _mousePosition);
        _destination = _mousePosition;
        _destinationAngle = _mouseAngle;
        double angleInDegrees = MathHelper.ToDegrees((float)_destinationAngle);
        bool isFacingRight = angleInDegrees >= -90 && angleInDegrees <= 90;
        _player.Facing = isFacingRight ? ActorFacing.Right : ActorFacing.Left;
        isMoving = true;
        _player.TransitionState(ActorState.Walking);
        return true;
    }

    public void OnLeftClickRelease()
    {
        //
    }

    private double CalculateAngle(Vector2 a, Vector2 b)
    {
        float deltaX = b.X - a.X;
        float deltaY = b.Y - a.Y;
        return Math.Atan2(deltaY, deltaX);
    }
}
