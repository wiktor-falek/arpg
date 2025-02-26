using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public enum ActorKind
{
    Player,
    Monster,
}

public enum ActorState
{
    Idling,
    Walking,
    Dead,
}

public enum ActorActionState
{
    None,
    Swinging,
    Casting,
}

public enum ActorFacing
{
    Left,
    Right,
}

public interface IActor
{
    string Id { get; }
    ActorKind Kind { get; }
    ActorState State { get; }
    ActorActionState ActionState { get; }
    ActorFacing Facing { get; }
    IActorAction? Action { get; }
    Vector2 Position { get; set; }
    ActorBaseStats Stats { get; }
    bool IsAlive { get; }
    IHitbox Hitbox { get; }

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void TakeDamage(double amount);
    bool TransitionState(ActorState newState);
}

public interface IMonster
{
    public int XP { get; }
    public bool IsLeashed { get; }
}

public interface IMonsterActor : IActor, IMonster;

public abstract class BaseActor : IActor
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public ActorKind Kind { get; }
    public ActorState State { get; private set; } = ActorState.Idling;
    public ActorActionState ActionState { get; private set; } = ActorActionState.None;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public IActorAction? Action { get; set; } = null;
    public IHitbox Hitbox => new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 16, 16, 32);
    public abstract ActorBaseStats Stats { get; }

    // int IMonster.XP { get; } = 10;

    public BaseActor(ActorKind kind)
    {
        Kind = kind;
    }

    public bool IsAlive => Stats.Health > 0;

    public abstract void Draw(SpriteBatch spriteBatch);

    public virtual void Update(GameTime gameTime)
    {
        if (!IsAlive)
        {
            TransitionState(ActorState.Dead);
        }
    }

    public void TakeDamage(double amount)
    {
        if (Stats.Health <= 0)
            return;
        Stats.OffsetHealth(-amount);
    }

    public virtual bool TransitionState(ActorState newState)
    {
        bool stateChanged = State != newState;
        if (stateChanged)
        {
            State = newState;
            // _graphicsComponent.ResetFrames();
        }

        return stateChanged;
    }

    public virtual bool TransitionState(ActorActionState newState)
    {
        bool stateChanged = ActionState != newState;
        if (stateChanged)
        {
            ActionState = newState;
            // _graphicsComponent.ResetFrames();
        }

        return stateChanged;
    }

    public void StartAction(IActorAction action, bool interruptPrevious = false)
    {
        if (interruptPrevious && Action is not null)
        {
            Action.Stop();
        }

        if (Action is null || Action.HasFinished)
        {
            Action = action;
            return;
        }
    }
}
