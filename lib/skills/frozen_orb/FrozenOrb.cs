public class FrozenOrb(IActor owner) : ISkill
{
    public string Name { get; } = "Frozen Orb";
    public Cooldown Cooldown = new(2f);
    private IActor _owner = owner;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
        {
            return;
        }

        FrozenOrbEntity frozenOrbEntity = new(_owner)
        {
            Position = new(_owner.Position.X, _owner.Position.Y),
            Angle = angle,
        };
        GameState.Entities.Add(frozenOrbEntity);
        Cooldown.StartCooldown();
    }
}
