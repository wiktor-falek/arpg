/*
    Next skill cannot be casted before previous skill cooldown hasnt finished
    Cooldown must not be updated while game is paused
*/

using Microsoft.Xna.Framework;

public class ActorCastAction : IActorAction
{
    public bool HasFinished { get; private set; } = false;
    private IActor _actor;
    private ISkill _skill;
    private double _angle;
    private double _elapsed = 0;

    public ActorCastAction(IActor actor, ISkill skill, double angle)
    {
        _actor = actor;
        _skill = skill;
        _angle = angle;
        _actor.ActionState = ActorActionState.Casting;
    }

    public void Update(GameTime gameTime)
    {
        if (HasFinished)
            return;

        _elapsed += gameTime.ElapsedGameTime.TotalSeconds;
        if (_elapsed >= _skill.BaseCastTime)
        {
            End();
        }
    }

    public bool Stop() => false;

    private void End()
    {
        HasFinished = true;
        _actor.ActionState = ActorActionState.None;
        _skill.Cast(_angle);
    }
}
