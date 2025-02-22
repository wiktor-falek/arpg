using System;
using Microsoft.Xna.Framework;

public class PlayerLootAction : IActorAction
{
    private static int PICKUP_RANGE = 40;
    private Player _player;
    private DroppedItem _item;
    private ActorMoveAction _moveAction;

    public PlayerLootAction(Player actor, DroppedItem item)
    {
        _player = actor;
        _item = item;
        _moveAction = new(
            _player,
            Utils.GetRadialIntersection(item.Position, _player.Position, PICKUP_RANGE - 1)
        );
    }

    public void Update(GameTime gameTime)
    {
        if (Vector2.Distance(_item.Position, _player.Position) < PICKUP_RANGE)
        {
            _item.GetPickedUp(_player);
            _player.TransitionState(ActorState.Idling);
            _player.Action = null;
        }
        else
        {
            _moveAction.Update(gameTime);
        }
    }
}
