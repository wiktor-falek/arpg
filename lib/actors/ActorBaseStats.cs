using System;
using Microsoft.Xna.Framework;

public class ActorBaseStats {
    public double Speed { get; set ; }
    public double Health { get; set; }
    public double MaxHealth { get; set; }
    public double HealthRegen { get; set; }
    public double HealthDegen { get; set; }
    public double Mana { get; set; }
    public double MaxMana { get; set; }
    public double ManaRegen { get; set; }
    public double ManaDegen { get; set; }

    private IActor _actor;
    private const double TICK_TIME = 0.1d;
    private double _regenTimer = 0f;

    public ActorBaseStats(IActor actor, double speed, double health, double mana = 0, double healthRegen = 0, double manaRegen = 0)
    {
        _actor = actor;
        Speed = speed;
        Health = MaxHealth = health;
        Mana = MaxMana = mana;
        HealthRegen = healthRegen;
        ManaRegen = manaRegen;
        HealthDegen = 0;
        ManaDegen = 0;
    }
    
    public void Update(GameTime gameTime)
    {
        _regenTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (_regenTimer >= TICK_TIME)
        {
            double netHealthChange = (HealthRegen - HealthDegen) * TICK_TIME;
            double netManaChange = (ManaRegen - ManaDegen) * TICK_TIME;

            if (netHealthChange < 0)
            {
                _actor.TakeDamage(-netHealthChange);
            }
            else
            {
                OffsetHealth(netHealthChange);
            }

            OffsetMana(netManaChange);

            _regenTimer -= TICK_TIME;
        }
    }

    public void OffsetHealth(double amount)
    {
        Health = Math.Clamp(Health + amount, 0, MaxHealth);
    }

    public void OffsetMana(double amount)
    {
        Mana = Math.Clamp(Mana + amount, 0, MaxMana);
    }

    public void AddHealthDegen(double damagePerSecond)
    {
        HealthDegen += damagePerSecond;
    }

    public void SubtractHealthDegen(double damagePerSecond)
    {
        HealthDegen -= damagePerSecond;
    }
}
