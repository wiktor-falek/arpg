using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbEntity(IActor owner) : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 110f;
    public readonly float MaxDuration = 3f;
    public double Angle = 0d;
    public float Rotation = 0f;
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
        Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 3f;
        _frozenOrbBehaviorComponent.Update(this, gameTime);
    }
}
