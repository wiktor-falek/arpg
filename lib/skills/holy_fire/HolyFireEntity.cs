using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HolyFireEntity : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public IActor Owner;
    public Vector2 Position { get; set; }
    public IHitbox Hitbox
    {
        get => new CircleHitbox(Position, Radius);
        set => _hitbox = value;
    }
    public double Radius = 100d;

    private IHitbox _hitbox;

    private HolyFireGraphicsComponent _holyFireGraphicsComponent;
    private HolyFireBehaviorComponent _holyFireBehaviorComponent;

    public HolyFireEntity(IActor owner)
    {
        Owner = owner;
        _holyFireGraphicsComponent = new(this);
        _holyFireBehaviorComponent = new();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _holyFireGraphicsComponent.Draw(this, spriteBatch);
    }

    public void Update(GameTime gameTime)
    {
        _holyFireBehaviorComponent.Update(this, gameTime);
    }
}
