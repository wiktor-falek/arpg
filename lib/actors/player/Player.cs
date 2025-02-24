using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Player : IActor
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public ActorKind Kind { get; } = ActorKind.Player;
    public ActorState State { get; private set; } = ActorState.Idling;
    public ActorActionState ActionState { get; set; } = ActorActionState.None;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public IActorAction? Action { get; private set; } = null;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public bool IsAlive => Stats.Health > 0;
    public IHitbox Hitbox
    {
        get => new RectangleHitbox((int)Position.X - 12, (int)Position.Y - 24, 20, 50);
    }
    public Vector2 Size => new(140, 140);

    public SkillCollection Skills;
    public ActorBaseStats Stats { get; }
    public Inventory Inventory;
    public Equipment Equipment;

    public PlayerInputComponent InputComponent;
    private PlayerGraphicsComponent _graphicsComponent;

    public Player()
    {
        Skills = new(this);
        Stats = new PlayerStats(
            this,
            speed: 100,
            health: 100,
            mana: 100,
            healthRegen: 0,
            manaRegen: 0
        );
        Inventory = new(this);
        Equipment = new(this);

        Equipment.Equip(new Sandals().ToMagic());
        Equipment.Equip(new Hood().ToRare());
        Equipment.Equip(new TheOneRubyRing());

        InputComponent = new(this);
        _graphicsComponent = new(this);
    }

    public void Update(GameTime gameTime)
    {
        Skills.Update(gameTime);
        Stats.Update(gameTime);
        InputComponent.Update(gameTime);
        Action?.Update(gameTime);
        _graphicsComponent.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _graphicsComponent.Draw(spriteBatch);
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

    public void StartAction(IActorAction action, bool interruptPrevious = false)
    {
        if (interruptPrevious && Action is not null)
        {
            Action.Stop();
        }

        if (Action is null || Action.HasFinished)
        {
            Action = action;
            return;
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
