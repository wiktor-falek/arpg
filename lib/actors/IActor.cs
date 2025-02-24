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
    ActorActionState ActionState { get; set; }
    ActorFacing Facing { get; set; }
    IActorAction? Action { get; }
    Vector2 Position { get; set; }
    ActorBaseStats Stats { get; }
    bool IsAlive { get; }
    IHitbox Hitbox { get; }

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void TakeDamage(double amount);
    void TransitionState(ActorState newState);
}

public interface IMonster
{
    public int XP { get; }
}

public interface IMonsterActor : IActor, IMonster;
