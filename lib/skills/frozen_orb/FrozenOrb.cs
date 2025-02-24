public class FrozenOrb(IActor owner) : ISkill
{
    public IActor Owner { get; } = owner;
    public string Name { get; } = "Frozen Orb";
    public Cooldown Cooldown { get; } = new(0.5d);
    public double BaseCastTime { get; } = 0.15d;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
        {
            return;
        }

        FrozenOrbEntity frozenOrbEntity = new(Owner)
        {
            Position = new(Owner.Position.X, Owner.Position.Y),
            Angle = angle,
        };
        Cooldown.StartCooldown();
    }
}
