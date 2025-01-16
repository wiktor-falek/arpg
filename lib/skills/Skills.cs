public class SkillCollection(IActor owner)
{
    public Fireball Fireball = new(owner);
    public FrozenOrb FrozenOrb = new(owner);
    public HolyFire HolyFire = new(owner);
    private IActor _owner = owner;
}
