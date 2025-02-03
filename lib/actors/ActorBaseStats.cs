using System;
using Microsoft.Xna.Framework;

public class ActorBaseStats {
    public double Health { get; set; }
    public double MaxHealth { get; set; }
    public double HealthRegen { get; set; }
    public double HealthDegen { get; set; }
    public double Mana { get; set; }
    public double MaxMana { get; set; }
    public double ManaRegen { get; set; }
    public double ManaDegen { get; set; }

    private const double TICK_TIME = 0.25d;
    private double _regenTimer = 0f;

    public ActorBaseStats(double health, double mana = 0, double healthRegen = 0, double manaRegen = 0)
    {
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

            Health = Math.Clamp(Health + netHealthChange, 0, MaxHealth);
            Mana = Math.Clamp(Mana + netManaChange, 0, MaxHealth);

            _regenTimer -= TICK_TIME;
        }
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
