using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Skeleton : BaseActor, IMonster
{
    public int XP => 10;
    public bool IsLeashed { get; private set; } = false;
    public override ActorBaseStats Stats { get; }

    private SkeletonGraphicsComponent _graphicsComponent = new();
    private SkeletonBehaviorComponent _behaviorComponent = new();

    public Skeleton()
        : base(ActorKind.Monster)
    {
        Stats = new(this, speed: 90, health: 40);
    }

    public new void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Stats.Update(gameTime);

        if (!IsAlive)
        {
            TransitionState(ActorState.Dead);
        }

        _behaviorComponent.Update(this, gameTime);
        Action?.Update(gameTime);
        _graphicsComponent.Update(this, gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _graphicsComponent.Draw(this, spriteBatch);
    }

    public new void TakeDamage(double amount)
    {
        IsLeashed = true;
        base.TakeDamage(amount);

        if (Stats.Health <= 0)
        {
            Game1.World.Player.OnKill(this);
        }
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
