using System;
using Microsoft.Xna.Framework;

public class ActorBaseStats {
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int HealthRegen { get; set; }
    public int HealthDegen { get; set; }
    public int Mana { get; set; }
    public int MaxMana { get; set; }
    public int ManaRegen { get; set; }
    public int ManaDegen { get; set; }

    private const double REGEN_TICK_TIME = 0.25d;
    private double _regenTimer = 0f;

    public ActorBaseStats(int health, int mana = 0, int healthRegen = 0, int manaRegen = 0)
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
        if (_regenTimer >= REGEN_TICK_TIME)
        {
            Health = Math.Min(Health + HealthRegen, MaxHealth);
            Mana = Math.Min(Mana + ManaRegen, MaxMana);

            Health = Math.Max(Health - HealthDegen, 0);
            Mana = Math.Max(Mana - ManaDegen, 0);

            _regenTimer -= REGEN_TICK_TIME;
        }
    } 
}
