using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IEntity
{
    string Id { get; set; }
    Rectangle Hitbox { get; }

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch, GraphicsDevice device);
}
