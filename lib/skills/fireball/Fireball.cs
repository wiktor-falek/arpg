public class Fireball(IActor owner) : ISkill
{
    public IActor Owner { get; } = owner;
    public string Name { get; } = "Fireball";
    public Cooldown Cooldown { get; } = new(1.5d);
    public double BaseCastTime { get; } = 0.5d;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
        {
            return;
        }

        FireballEntity fireballEntity = new(Owner)
        {
            Position = new(Owner.Position.X, Owner.Position.Y),
            Angle = angle,
        };

        Cooldown.StartCooldown();
    }
}
