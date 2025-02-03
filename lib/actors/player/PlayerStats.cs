public class PlayerStats : ActorBaseStats
{
    public PlayerLevel Level { get; }
    public int HealthOnKill { get; set; }
    public int ManaOnKill { get; set; }

    public PlayerStats(
        IActor actor,
        double speed,
        double health,
        double mana = 0,
        double healthRegen = 0,
        double manaRegen = 0,
        int healthOnKill = 0,
        int manaOnKill = 0
        ): base(actor, speed, health, mana, healthRegen, manaRegen)
    {
        Level = new();
        HealthOnKill = healthOnKill;
        ManaOnKill = manaOnKill;
    }
}
