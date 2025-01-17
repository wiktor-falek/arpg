using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
    ActorState State { get; }
    ActorActionState ActionState { get; }
    ActorFacing Facing { get; }
    Vector2 Position { get; }
    float Speed { get; }
    int Health { get; }
    int MaxHealth { get; }
    bool IsAlive { get; }
    IHitbox Hitbox { get; }

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, GraphicsDevice device);
    void TakeDamage(float amount);
}
