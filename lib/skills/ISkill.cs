public interface ISkill
{
    string Name { get; }
    public IActor Owner { get; }
    Cooldown Cooldown { get; }
    double BaseCastTime { get; }
    void Cast(double angle);
}
