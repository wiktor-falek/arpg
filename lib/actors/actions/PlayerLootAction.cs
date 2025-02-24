using System;
using Microsoft.Xna.Framework;

public class PlayerLootAction : IActorAction
{
    public bool HasFinished { get; private set; } = false;
    private Player _player;
    private DroppedItem _item;
    private ActorMoveAction _moveAction;
    private static int PICKUP_RANGE = 40;

    public PlayerLootAction(Player actor, DroppedItem item)
    {
        _player = actor;
        _item = item;
        _moveAction = new(
            _player,
            Utils.GetRadialIntersection(_item.Position, _player.Position, PICKUP_RANGE - 1)
        );
    }

    public void Update(GameTime gameTime)
    {
        if (HasFinished)
            return;

        if (Vector2.Distance(_item.Position, _player.Position) < PICKUP_RANGE)
        {
            _item.GetPickedUp(_player);
            Stop();
        }
        else
        {
            _moveAction.Update(gameTime);
        }
    }

    public bool Stop()
    {
        _moveAction.Stop();
        HasFinished = true;
        return true;
    }
}
