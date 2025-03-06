using Microsoft.Xna.Framework;

public class ActorCastAction : IActorAction
{
    public ActionState State { get; private set; } = ActionState.Pending;
    private IActor _actor;
    private ISkill _skill;
    private double _angle;
    private double _elapsed = 0;

    public ActorCastAction(IActor actor, ISkill skill, double angle)
    {
        _actor = actor;
        _skill = skill;
        _angle = angle;
    }

    public void Update(GameTime gameTime)
    {
        if (State != ActionState.Ongoing)
            return;

        _elapsed += gameTime.ElapsedGameTime.TotalSeconds;
        if (_elapsed >= _skill.BaseCastTime)
        {
            End();
        }
    }

    public void Start()
    {
        State = ActionState.Ongoing;
        _actor.TransitionState(ActorActionState.Casting);
    }

    public bool Stop() => false;

    private void End()
    {
        State = ActionState.Finished;
        _actor.TransitionState(ActorActionState.None);
        _skill.Cast(_angle);
    }
}
