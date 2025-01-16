using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IEntity
{
    string Id { get; set; }
    IHitbox Hitbox { get; }
    public Vector2 Position { get; set; }

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, GraphicsDevice device);
}
