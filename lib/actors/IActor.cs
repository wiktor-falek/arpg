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
    ActorFacing Facing { get; set; }
    IActorAction? Action { get; set; }
    Vector2 Position { get; set; }
    ActorBaseStats Stats { get; }
    bool IsAlive { get; }
    IHitbox Hitbox { get; }

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void TakeDamage(double amount);
    bool TransitionState(ActorState newState);
    bool TransitionState(ActorActionState newState);
}

public interface IMonster
{
    public int XP { get; }
    public int Level { get; }
    public bool IsLeashed { get; set; }
    public void OnDeath();
}

public interface IMonsterActor : IActor, IMonster;

public abstract class BaseActor : IActor
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public ActorKind Kind { get; }
    public ActorState State { get; private set; } = ActorState.Idling;
    public ActorActionState ActionState { get; set; } = ActorActionState.None;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public IActorAction? Action { get; set; } = null;
    public IHitbox Hitbox => new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 16, 16, 32);
    public abstract ActorBaseStats Stats { get; }

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
        }

        return stateChanged;
    }

    public virtual bool TransitionState(ActorActionState newState)
    {
        bool stateChanged = ActionState != newState;
        if (stateChanged)
        {
            ActionState = newState;
        }

        return stateChanged;
    }

    public void SetAction(IActorAction? action)
    {
        if (Action is null || Action.State == global::ActionState.Finished || Action.Stop())
        {
            Action = action;
            return;
        }
    }
}

public abstract class Monster : BaseActor, IMonster
{
    public int Level { get; private set; }
    public int XP { get; private set; }
    public bool IsLeashed { get; set; }

    public Monster(int level, int xp, bool isLeashed = false)
        : base(ActorKind.Monster)
    {
        Level = level;
        XP = xp;
        IsLeashed = isLeashed;
    }

    /// <summary>
    /// Drops items from global pool
    /// </summary>
    public void OnDeath()
    {
        Item item = GlobalLootTable.GenerateItem(Level);
        DroppedItem droppedItem = new(item, new(Position.X, Position.Y));
        Game1.World.Items.Add(droppedItem);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public new void TakeDamage(double amount)
    {
        base.TakeDamage(amount);

        IsLeashed = true;

        if (!IsAlive)
        {
            Game1.World.Player.OnKill(this);
            OnDeath();
        }
    }
}
