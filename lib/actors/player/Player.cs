using System;
using arpg;
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
        _graphicsComponent = new(this);
        InputComponent = new(this);
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

        Inventory.AddItem(new Item("Item", Rarity.Normal, 2, 3, Assets.Items.None_2x3), 0, 0);
        Inventory.AddItem(new Item("Item", Rarity.Normal, 2, 2, Assets.Items.None_2x2), 0, 3);
        Inventory.AddItem(new Item("Item", Rarity.Normal, 2, 1, Assets.Items.None_2x1), 2, 0);
        Inventory.AddItem(new Item("Item", Rarity.Normal, 1, 1, Assets.Items.None_1x1), 2, 1);
        Equipment.Equip(new Sandals().ToMagic());
        Equipment.Equip(new RubyRing().ToMagic());
        Equipment.Equip(new RubyRing().ToMagic());
    }

    public void Update(GameTime gameTime)
    {
        Stats.Update(gameTime);
        InputComponent.Update(gameTime);
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
