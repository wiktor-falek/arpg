using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbSecondaryEntity : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 175f;
    public double Angle = 0d;
    public float Damage = 10f;
    public readonly float MaxDuration = 1.5f;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox((int)Position.X - 16, (int)Position.Y - 16, 32, 32);
        // set => _hitbox = value;
    }
    private FrozenOrbSecondaryGraphicsComponent _frozenOrbSecondaryGraphicsComponent = new();
    private FrozenOrbSecondaryBehaviorComponent _frozenOrbSecondaryBehaviorComponent = new();

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
    {
        _frozenOrbSecondaryGraphicsComponent.Draw(this, spriteBatch, device);
    }

    public void Update(GameTime gameTime)
    {
        _frozenOrbSecondaryBehaviorComponent.Update(this, gameTime);
    }
}
