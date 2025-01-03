using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IHudElement
{
    string Id { get; set; }
    Vector2 Position { get; set; }
    Vector2 Size { get; set; }
    Rectangle Rectangle { get; }

    void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
    void Update();
};
