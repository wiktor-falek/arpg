public class MovementSpeedAffix : Affix
{
    public MovementSpeedAffix(int minRange, int? maxRange = null)
        : base(minRange, maxRange) { }

    public override void Apply(Player player, int value)
    {
        player.Stats.Speed += value;
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
