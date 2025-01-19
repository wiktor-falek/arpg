using System.Numerics;

public class Monster
{
    public ActorState State { get; set; } = ActorState.Idling;
    public ActorActionState ActionState { get; set; } = ActorActionState.None;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public bool IsLeashed = false;
}
