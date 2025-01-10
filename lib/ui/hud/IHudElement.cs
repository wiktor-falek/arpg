using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IHudElement
{
    void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
    void Update(GameTime gameTime);
};
