using System;
using Microsoft.Xna.Framework;

public class ActorMoveAction : IActorAction
{
    private IActor _actor;
    private Vector2 _destination;

    public ActorMoveAction(IActor actor, Vector2 destination)
    {
        _actor = actor;
        _destination = destination;
        SetFacingState();
        _actor.TransitionState(ActorState.Walking);
    }

    public void Update(GameTime gameTime)
    {
        bool hasReachedDestination = WalkTowardsDestination(gameTime);

        if (hasReachedDestination)
        {
            _actor.TransitionState(ActorState.Idling);
            _actor.Action = null;
        }
    }

    public void SetDestination(Vector2 destination)
    {
        _destination = destination;
        SetFacingState();
    }

    private bool WalkTowardsDestination(GameTime gameTime)
    {
        double elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;

        float distanceToDestination = Vector2.Distance(_actor.Position, _destination);
        if (distanceToDestination > 1f)
        {
            double angle = Utils.CalculateAngle(_actor.Position, _destination);
            float x =
                _actor.Position.X + (float)(_actor.Stats.Speed * elapsedTime * Math.Cos(angle));
            float y =
                _actor.Position.Y + (float)(_actor.Stats.Speed * elapsedTime * Math.Sin(angle));
            _actor.Position = new(x, y);
            return false;
        }
        else
        {
            _actor.Position = _destination;
            return true;
        }
    }

    private void SetFacingState()
    {
        double angle = Utils.CalculateAngleInDegrees(_actor.Position, _destination);
        bool isFacingRight = angle >= -90 && angle <= 90;
        _actor.Facing = isFacingRight ? ActorFacing.Right : ActorFacing.Left;
    }
}
