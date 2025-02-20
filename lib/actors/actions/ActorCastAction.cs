/*
    Next skill cannot be casted before previous skill cooldown hasnt finished
    Cooldown must not be updated while game is paused
*/

public class ActorCastAction
{
    private IActor _actor;
    private ISkill _skill;

    public ActorCastAction(IActor actor, ISkill skill)
    {
        _actor = actor;
        _skill = skill;
    }
}
