using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbEntity(IActor owner) : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 350f;
    public double Angle = 0d;

    // public float Damage = 10f;
    public readonly float MaxDuration = 3f;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox(0, 0, 0, 0);
    }

    private IActor _owner = owner;

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device) { }

    public void Update(GameTime gameTime) { }
}
