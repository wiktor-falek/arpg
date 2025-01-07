using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Player : IActor
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public ActorState State { get; set; } = ActorState.Idling;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public float Speed { get; set; } = 250f;
    public int Health { get; set; } = 500;
    public int MaxHealth { get; set; } = 500;
    public bool IsAlive => Health > 0;
    public Rectangle Hitbox
    {
        get => new((int)Position.X - 12, (int)Position.Y - 24, 20, 50);
    }
    public Vector2 Size => new(140, 140);

    private PlayerInputComponent _inputComponent = new();
    private PlayerGraphicsComponent _graphicsComponent = new();

    public void Update(GameTime gameTime)
    {
        _inputComponent.Update(this, gameTime);
        _graphicsComponent.Update(this, gameTime);
    }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
    {
        _graphicsComponent.Draw(this, spriteBatch, device, showHitbox: true);
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

    public void Attack(double angle)
    {
        Fireball fireball = new() { Position = new(Position.X, Position.Y), Angle = angle };
        Game1.Entities.Add(fireball);
    }

    public void TakeDamage(float amount)
    {
        Health -= (int)Math.Floor(amount);
        Health = Math.Max(Health, 0);
    }
}
