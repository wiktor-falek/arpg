using Microsoft.Xna.Framework;

public class SkillCollection(IActor owner)
{
    public Fireball Fireball = new(owner);
    public FrozenOrb FrozenOrb = new(owner);
    public HolyFire HolyFire = new(owner);
    private IActor _owner = owner;

    public void Update(GameTime gameTime)
    {
        Fireball.Cooldown.Update(gameTime);
        FrozenOrb.Cooldown.Update(gameTime);
        HolyFire.Cooldown.Update(gameTime);
    }
}
