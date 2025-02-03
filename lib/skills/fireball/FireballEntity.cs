using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FireballEntity : IEntity
{
    public IActor Owner;
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 300f;
    public double Angle = 0d;
    public float Damage = 10f;
    public readonly float MaxDuration = 2f;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 8, 16, 16);
        set => _hitbox = value;
    }

    private IHitbox _hitbox;
    private FireballGraphicsComponent _fireballGraphicsComponent = new();
    private FireballBehaviorComponent _fireballBehaviorComponent = new();

    public FireballEntity(IActor owner)
    {
        Owner = owner;
        GameState.Entities.Add(this);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _fireballGraphicsComponent.Draw(this, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        _fireballBehaviorComponent.Update(this, gameTime);
    }

    public void Destroy()
    {
        GameState.RemoveEntity(this);
    }
}
