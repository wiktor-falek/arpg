using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Monster : IActor
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public ActorState State { get; set; } = ActorState.Idling;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public float Speed { get; set; } = 100f;
    public int Health { get; set; } = 100;
    public int MaxHealth { get; set; } = 1000;
    public bool IsAlive => Health > 0;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 16, 16, 32);
    }

    private MonsterGraphicsComponent _graphicsComponent = new();
    private MonsterBehaviorComponent _behaviorComponent = new();

    public void Update(GameTime gameTime)
    {
        if (!IsAlive)
        {
            TransitionState(ActorState.Dead);
        }

        _graphicsComponent.Update(this, gameTime);
        _behaviorComponent.Update(this, gameTime);
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
    {
        _graphicsComponent.Draw(this, spriteBatch, device);
    }

    public void Attack(double angle) { }

    public void TakeDamage(float amount)
    {
        Health -= (int)Math.Floor(amount);
        Health = Math.Max(Health, 0);
    }

    public void TransitionState(ActorState newState)
    {
        bool stateChanged = State != newState;
        if (stateChanged)
        {
            State = newState;
            _graphicsComponent.ResetFrames();
        }
    }
}
