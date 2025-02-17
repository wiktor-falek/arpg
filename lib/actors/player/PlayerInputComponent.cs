using System;
using arpg;
using Microsoft.Xna.Framework;

public class PlayerInputComponent
{
    private Player _player;

    private bool _isMoving = false;
    private bool _isHoldingLeftClick;
    private Vector2 _destination;
    private double _destinationAngle = 0d;
    private Vector2 _playerAimCoordinate;
    private double _playerAimAngle = 0d;
    private ISkill? _heldSkill = null;

    public PlayerInputComponent(Player player)
    {
        _player = player;

        // TODO: refactor
        Game1.InputManager.OnPress(
            RemappableGameAction.CastBarOne,
            () => HoldSkill(player.Skills.Fireball)
        );
        Game1.InputManager.OnRelease(
            RemappableGameAction.CastBarOne,
            () => ReleaseSkill(player.Skills.Fireball)
        );

        Game1.InputManager.OnPress(
            RemappableGameAction.CastBarTwo,
            () => HoldSkill(player.Skills.FrozenOrb)
        );
        Game1.InputManager.OnRelease(
            RemappableGameAction.CastBarTwo,
            () => ReleaseSkill(player.Skills.FrozenOrb)
        );

        // TODO: figure out what the behavior for instant/toggle skills should be
        Game1.InputManager.OnPress(
            RemappableGameAction.CastBarThree,
            () =>
            {
                if (GameState.IsRunning)
                {
                    player.Skills.HolyFire.Cast(_playerAimAngle);
                }
            }
        );
    }

    private void HoldSkill(ISkill skill)
    {
        _heldSkill = skill;
    }

    private void ReleaseSkill(ISkill skill)
    {
        if (_heldSkill == skill)
            _heldSkill = null;
    }

    public void Update(GameTime gameTime)
    {
        if (!GameState.IsRunning)
            return;

        // TODO: prevent clicks outside of window
        _playerAimCoordinate = Camera.CameraOrigin + MouseManager.GetInGameMousePosition();
        _playerAimAngle = CalculateAngle(_player.Position, _playerAimCoordinate);

        _heldSkill?.Cast(_playerAimAngle);

        if (_isHoldingLeftClick)
            StartMove();

        if (!_isMoving)
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
            _isMoving = false;
            _player.TransitionState(ActorState.Idling);
        }
    }

    public bool OnLeftClick()
    {
        if (!GameState.IsRunning)
            return false;

        _isHoldingLeftClick = true;

        _destination = _playerAimCoordinate;
        _destinationAngle = _playerAimAngle;
        _isMoving = true;

        double angleInDegrees = MathHelper.ToDegrees((float)_destinationAngle);
        bool isFacingRight = angleInDegrees >= -90 && angleInDegrees <= 90;
        _player.Facing = isFacingRight ? ActorFacing.Right : ActorFacing.Left;
        _player.TransitionState(ActorState.Walking);
        return true;
    }

    public bool OnLeftClickRelease()
    {
        _isHoldingLeftClick = false;
        return true;
    }

    public void StartMove(Vector2? aimCoordinate = null)
    {
        // TODO: refactor

        _isMoving = true;
        _destination = aimCoordinate ?? _playerAimCoordinate;
        _destinationAngle =
            aimCoordinate != null
                ? CalculateAngle(_player.Position, aimCoordinate.Value)
                : _playerAimAngle;

        double angleInDegrees = MathHelper.ToDegrees((float)_destinationAngle);
        bool isFacingRight = angleInDegrees >= -90 && angleInDegrees <= 90;
        _player.Facing = isFacingRight ? ActorFacing.Right : ActorFacing.Left;
        _player.TransitionState(ActorState.Walking);
    }

    private double CalculateAngle(Vector2 a, Vector2 b)
    {
        float deltaX = b.X - a.X;
        float deltaY = b.Y - a.Y;
        return Math.Atan2(deltaY, deltaX);
    }
}
