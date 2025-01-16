public class HolyFire(IActor owner) : ISkill
{
    public string Name { get; } = "Holy Fire";
    public Cooldown Cooldown = new(1f);
    private IActor _owner = owner;
    private bool isActive = false;
    private HolyFireEntity _entity;

    public void Cast()
    {
        if (!Cooldown.CanCast())
            return;

        if (!isActive)
        {
            HolyFireEntity holyFireEntity = new(_owner)
            {
                Position = new(_owner.Position.X, _owner.Position.Y),
                Radius = 100f,
            };
            GameState.Entities.Add(holyFireEntity);

            _entity = holyFireEntity;
            isActive = true;
        }
        else
        {
            GameState.Entities.Remove(_entity);
            isActive = false;
        }

        Cooldown.StartCooldown();
    }
}
