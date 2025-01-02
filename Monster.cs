using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Monster : IActor
{
    public ActorState State { get; set; } = ActorState.Idling;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public float Speed { get; set; } = 200f;
    public int Health { get; set; } = 100;
    public readonly int MaxHealth = 100;

    private MonsterGraphicsComponent _graphicsComponent = new();

    public void LoadAssets(ContentManager content)
    {
        _graphicsComponent.LoadAssets(content);
    }

    public void Update(GameTime gameTime)
    {
        _graphicsComponent.Update(this, gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _graphicsComponent.Draw(this, spriteBatch);
    }

    public void Attack(IActor target)
    {
        float amount = 10;
        target.TakeDamage(amount);
    }

    public void TakeDamage(float amount)
    {
        Health -= (int)Math.Floor(amount);
        Health = Math.Max(Health, 0);
    }
}
