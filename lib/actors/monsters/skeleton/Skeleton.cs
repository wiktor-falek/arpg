using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Skeleton : Monster
{
    private SkeletonGraphicsComponent _graphicsComponent = new();
    private SkeletonBehaviorComponent _behaviorComponent = new();
    public override ActorBaseStats Stats { get; }

    public Skeleton(int level)
        : base(level, xp: 10 + (level - 1) * 2)
    {
        Stats = new(this, speed: 90, health: 40 + (Level - 1) * 10);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Stats.Update(gameTime);
        _behaviorComponent.Update(this, gameTime);
        if (Action is not null)
        {
            if (Action.State == global::ActionState.Pending)
                Action.Start();
            Action.Update(gameTime);
        }
        _graphicsComponent.Update(this, gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _graphicsComponent.Draw(this, spriteBatch);
    }

    public new void TransitionState(ActorState newState)
    {
        bool stateChanged = base.TransitionState(newState);
        if (stateChanged)
        {
            _graphicsComponent.ResetFrames();
        }
    }

    public new void TransitionState(ActorActionState newState)
    {
        bool stateChanged = base.TransitionState(newState);
        if (stateChanged)
        {
            _graphicsComponent.ResetFrames();
        }
    }
}
