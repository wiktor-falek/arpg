using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HolyFire : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public IHitbox Hitbox
    {
        get => new CircleHitbox(Position, Radius);
        set => _hitbox = value;
    }
    public double Radius = 100d;

    private IHitbox _hitbox;

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device) { }

    public void Update(GameTime gameTime) { }
}
