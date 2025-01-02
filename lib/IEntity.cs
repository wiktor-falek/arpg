using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public interface IEntity
{
  string Id { get; set; }
  void LoadAssets(ContentManager contentManager);
  void Update(GameTime gameTime);
  void Draw(SpriteBatch spriteBatch, GraphicsDevice device);
}
