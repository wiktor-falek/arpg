public class SkillCollection(IActor owner)
{
    public Fireball Fireball = new(owner);
    private IActor _owner = owner;
}
