using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public enum ActorState
{
    Idling,
    Walking,
    Dead,
}

public enum ActorFacing
{
    Left,
    Right,
}

public interface IActor
{
    string Id { get; set; }
    ActorState State { get; set; }
    ActorFacing Facing { get; set; }
    Vector2 Position { get; set; }
    float Speed { get; set; }
    int Health { get; set; }
    int MaxHealth { get; set; }
    bool IsAlive { get; }
    IHitbox Hitbox { get; }

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, GraphicsDevice device);
    void TakeDamage(float amount);
}
