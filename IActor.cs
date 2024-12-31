using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public enum ActorState
{
    Idling,
    Walking,
}

public enum ActorFacing
{
    Left,
    Right,
}

public interface IActor
{
    ActorState State { get; set; }
    ActorFacing Facing { get; set; }
    Vector2 Position { get; set; }
    float Speed { get; set; }
    uint Health { get; set; }

    void LoadAssets(ContentManager contentManager);
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void Attack();
}
