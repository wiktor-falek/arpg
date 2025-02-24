public class HolyFire(IActor owner) : ISkill
{
    public IActor Owner { get; } = owner;
    public string Name { get; } = "Holy Fire";
    public Cooldown Cooldown { get; } = new(1d);
    public double BaseCastTime { get; } = 0d;
    private bool isActive = false;
    private HolyFireEntity _entity;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
            return;

        if (!isActive)
        {
            HolyFireEntity holyFireEntity = new(Owner)
            {
                Position = new(Owner.Position.X, Owner.Position.Y),
                Radius = 100f,
            };

            _entity = holyFireEntity;
            isActive = true;
        }
        else
        {
            _entity.Destroy();
            isActive = false;
        }

        Cooldown.StartCooldown();
    }
}
