using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Fireball : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 350f;
    public double Angle = 0d;
    public Rectangle Hitbox
    {
        get => new((int)Position.X - 8, (int)Position.Y - 8, 16, 16);
    }
    public float Damage = 10f;
    public readonly float MaxDuration = 2f;

    private FireballGraphicsComponent _fireballGraphicsComponent = new();
    private FireballBehaviorComponent _fireballBehaviorComponent = new();

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
    {
        _fireballGraphicsComponent.Draw(this, spriteBatch, device, showHitbox: false);
    }

    public void Update(GameTime gameTime)
    {
        _fireballBehaviorComponent.Update(this, gameTime);
    }
}
