using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbSecondaryEntity : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 175f;
    public double Angle = 0d;
    public readonly float MaxDuration = 1f;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox(0, 0, 0, 0);
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
