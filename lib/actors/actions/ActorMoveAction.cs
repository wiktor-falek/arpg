using System;
using Microsoft.Xna.Framework;

public class ActorMoveAction : IActorAction
{
    public bool HasFinished { get; private set; } = false;
    private IActor _actor;
    private Vector2 _destination;
    private double _angle;

    public ActorMoveAction(IActor actor, Vector2 destination)
    {
        _actor = actor;
        SetDestination(destination);
    }

    public void Update(GameTime gameTime)
    {
        if (HasFinished)
            return;

        bool hasReachedDestination = WalkTowardsDestination(gameTime);
        if (hasReachedDestination)
        {
            Stop();
        }
    }

    public bool Stop()
    {
        _actor.TransitionState(ActorState.Idling);
        HasFinished = true;
        return true;
    }

    public void SetDestination(Vector2 destination)
    {
        HasFinished = false;
        _destination = destination;
        _angle = Utils.CalculateAngle(_actor.Position, _destination);
        _actor.Facing = GetFacingState(_angle);
        _actor.TransitionState(ActorState.Walking);
    }

    private bool WalkTowardsDestination(GameTime gameTime)
    {
        double elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;

        float distanceToDestination = Vector2.Distance(_actor.Position, _destination);
        if (distanceToDestination > 1f)
        {
            float x =
                _actor.Position.X + (float)(_actor.Stats.Speed * elapsedTime * Math.Cos(_angle));
            float y =
                _actor.Position.Y + (float)(_actor.Stats.Speed * elapsedTime * Math.Sin(_angle));
            _actor.Position = new(x, y);
            return false;
        }
        else
        {
            _actor.Position = _destination;
            HasFinished = true;
            return true;
        }
    }

    private ActorFacing GetFacingState(double angle)
    {
        double degrees = MathHelper.ToDegrees((float)angle);
        bool isFacingRight = degrees >= -90 && degrees <= 90;
        return isFacingRight ? ActorFacing.Right : ActorFacing.Left;
    }
}
