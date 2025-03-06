using System;
using Microsoft.Xna.Framework;

public class PlayerLootAction : IActorAction
{
    public ActionState State { get; private set; } = ActionState.Pending;
    private Player _player;
    private DroppedItem _item;
    private ActorMoveAction _moveAction;
    private static int PICKUP_RANGE = 40;

    public PlayerLootAction(Player player, DroppedItem item)
    {
        _player = player;
        _item = item;
        _moveAction = new(player, item.Position);
    }

    public void Update(GameTime gameTime)
    {
        if (State != ActionState.Ongoing)
            return;

        if (Vector2.Distance(_item.Position, _player.Position) <= PICKUP_RANGE)
        {
            _item.GetPickedUp(_player);
            Stop();
        }
        else
        {
            _moveAction.Update(gameTime);
        }
    }

    public void Start()
    {
        State = ActionState.Ongoing;
        _moveAction.Start();
    }

    public bool Stop()
    {
        State = ActionState.Finished;
        _moveAction.Stop();
        return true;
    }
}
