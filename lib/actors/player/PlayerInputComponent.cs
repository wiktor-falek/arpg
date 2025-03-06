using System;
using System.Diagnostics.Tracing;
using arpg;
using Microsoft.Xna.Framework;

// TODO: prevent clicks outside of window
public class PlayerInputComponent
{
    private Player _player;
    private bool _isHoldingLeftClick = false;
    private ISkill? _heldSkill; // TODO: consider a List

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

        Game1.InputManager.OnPress(
            RemappableGameAction.CastBarThree,
            () =>
            {
                if (GameState.IsRunning)
                {
                    StartCasting(player.Skills.HolyFire);
                }
            }
        );
    }

    private void HoldSkill(ISkill skill)
    {
        _heldSkill = skill;
        StartCasting(_heldSkill);
    }

    private void ReleaseSkill(ISkill skill)
    {
        if (_heldSkill == skill)
            _heldSkill = null;
    }

    private void StartCasting(ISkill skill)
    {
        Vector2 playerAimCoordinate = Camera.CameraOrigin + MouseManager.GetInGameMousePosition();
        double angle = Utils.CalculateAngle(_player.Position, playerAimCoordinate);
        ActorCastAction action = new(_player, skill, angle);
        _player.SetAction(action);
    }

    public void Update(GameTime gameTime)
    {
        if (_heldSkill is not null)
        {
            StartCasting(_heldSkill);
        }

        if (_isHoldingLeftClick)
        {
            Move();
        }
    }

    public void Move()
    {
        Vector2 destination = Camera.CameraOrigin + MouseManager.GetInGameMousePosition();
        ActorMoveAction action = new(_player, destination);
        _player.SetAction(action);
    }

    public bool OnLeftClick()
    {
        _isHoldingLeftClick = true;
        Move();
        return true;
    }

    public bool OnLeftClickRelease()
    {
        _isHoldingLeftClick = false;
        return true;
    }
}
