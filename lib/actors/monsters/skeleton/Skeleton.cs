using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Skeleton : IActor
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public ActorKind Kind { get; } = ActorKind.Monster;
    public ActorState State { get; set; } = ActorState.Idling;
    public ActorActionState ActionState { get; set; } = ActorActionState.None;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public float Speed { get; } = 90f;
    public int Health { get; private set; } = 100;
    public int MaxHealth { get; } = 1000;
    public bool IsAlive => Health > 0;
    public bool IsLeashed = true;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox((int)Position.X - 8, (int)Position.Y - 16, 16, 32);
    }

    private SkeletonGraphicsComponent _graphicsComponent = new();
    private SkeletonBehaviorComponent _behaviorComponent = new();

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

    public void TakeDamage(float amount)
    {
        Health -= (int)Math.Floor(amount);
        Health = Math.Max(Health, 0);
        IsLeashed = true;
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

    public void TransitionState(ActorActionState newState)
    {
        bool stateChanged = ActionState != newState;
        if (stateChanged)
        {
            ActionState = newState;
            _graphicsComponent.ResetFrames();
        }
    }
}
