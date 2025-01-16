public class Fireball(IActor owner): ISkill
{
    public string Name { get; }= "Fireball";
    public Cooldown Cooldown = new(2f);
    private IActor _owner = owner;

    public void Cast(double angle)
    {
        if (!Cooldown.CanCast())
        {
            return;
        }

        FireballEntity fireballEntity = new() { 
            Position = new(_owner.Position.X, _owner.Position.Y), Angle = angle
        };
        GameState.Entities.Add(fireballEntity);
        Cooldown.StartCooldown();
    }
}
