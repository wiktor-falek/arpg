using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Player : IActor
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public ActorKind Kind { get; } = ActorKind.Player;
    public ActorState State { get; set; } = ActorState.Idling;
    public ActorActionState ActionState { get; set; } = ActorActionState.None;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public ActorBaseStats Stats { get; }
    public bool IsAlive => Stats.Health > 0;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox((int)Position.X - 12, (int)Position.Y - 24, 20, 50);
    }
    public Vector2 Size => new(140, 140);

    public SkillCollection Skills;

    private PlayerInputComponent _inputComponent = new();
    private PlayerGraphicsComponent _graphicsComponent = new();

    public Player()
    {
        Skills = new(this);
        Stats = new PlayerStats(this, speed: 100, health: 100, mana: 100, healthRegen: 0, manaRegen: 2, healthOnKill: 1);
    }

    public void Update(GameTime gameTime)
    {
        Stats.Update(gameTime);
        _inputComponent.Update(this, gameTime);
        _graphicsComponent.Update(this, gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _graphicsComponent.Draw(this, spriteBatch);
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

    public void TakeDamage(double amount)
    {
        Stats.OffsetHealth(-amount);
    }

    public void OnKill(IMonsterActor monster)
    {
        PlayerStats playerStats = (PlayerStats)Stats;
        playerStats.Level.GrantXP(monster.XP);
        playerStats.OffsetHealth(playerStats.HealthOnKill);
        playerStats.OffsetMana(playerStats.ManaOnKill);
    }
}
