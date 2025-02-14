public class LifeAffix : Affix
{
    public LifeAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.MaxHealth += value;
    }
}

public class ManaAffix : Affix
{
    public ManaAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.MaxMana += value;
    }
}

public class EvasionAffix : Affix
{
    public EvasionAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.Evasion += value;
    }
}

public class ArmorAffix : Affix
{
    public ArmorAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.Armor += value;
    }
}

public class MovementSpeedAffix : Affix
{
    public MovementSpeedAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.Speed += value;
    }
}

public class LifeOnKillAffix : Affix
{
    public LifeOnKillAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).HealthOnKill += value;
    }
}

public class ManaOnKillAffix : Affix
{
    public ManaOnKillAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        ((PlayerStats)player.Stats).ManaOnKill += value;
    }
}
