using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Player : BaseActor
{
    public override PlayerStats Stats { get; }
    public SkillCollection Skills;
    public Inventory Inventory;
    public Equipment Equipment;

    public PlayerInputComponent InputComponent;
    private PlayerGraphicsComponent _graphicsComponent;

    public Player()
        : base(ActorKind.Player)
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

    public new void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Skills.Update(gameTime);
        Stats.Update(gameTime);
        InputComponent.Update(gameTime);
        Action?.Update(gameTime);
        _graphicsComponent.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _graphicsComponent.Draw(spriteBatch);
    }

    public new void TransitionState(ActorState newState)
    {
        bool stateChanged = base.TransitionState(newState);
        if (stateChanged)
        {
            _graphicsComponent.ResetFrames();
        }
    }

    public void OnKill(IMonster monster)
    {
        Stats.Level.GrantXP(monster.XP);
        Stats.OffsetHealth(Stats.HealthOnKill);
        Stats.OffsetMana(Stats.ManaOnKill);
    }
}
