using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HolyFireEntity(IActor owner) : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public IActor Owner = owner;
    public Vector2 Position { get; set; }
    public IHitbox Hitbox
    {
        get => new CircleHitbox(Position, Radius);
        set => _hitbox = value;
    }
    public double Radius = 50f;

    private IHitbox _hitbox;

    private HolyFireGraphicsComponent _holyFireGraphicsComponent = new();
    private HolyFireBehaviorComponent _holyFireBehaviorComponent = new();

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
    {
        _holyFireGraphicsComponent.Draw(this, spriteBatch, device);
    }

    public void Update(GameTime gameTime)
    {
        _holyFireBehaviorComponent.Update(this, gameTime);
    }
}
