using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbEntity(IActor owner) : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 0f;
    public readonly float MaxDuration = 3f;
    public double Angle = 0d;
    public double Rotation = 0d;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox(0, 0, 0, 0);
    }

    private IActor _owner = owner;
    private FrozenOrbGraphicsComponent _frozenOrbGraphicsComponent = new();
    private FrozenOrbBehaviorComponent _frozenOrbBehaviorComponent = new();

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
    {
        _frozenOrbGraphicsComponent.Draw(this, spriteBatch, device);
    }

    public void Update(GameTime gameTime)
    {
        _frozenOrbBehaviorComponent.Update(this, gameTime);
    }
}
