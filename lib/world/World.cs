using System;
using System.Collections.Generic;
using System.Threading.Channels;
using arpg;
using Microsoft.Xna.Framework;

public class World(Player player)
{
    public readonly Player Player = player;
    public readonly List<DroppedItem> Items =
    [
        new DroppedItem(new Hood(), new(100, 100)),
        new DroppedItem(new Sandals(), new(100, 200)),
        new DroppedItem(
            new Item("Mirror of Kalandra", Rarity.Normal, 1, 1, Assets.Items.None_1x1),
            new(0, 0)
        ),
    ];
    public readonly List<IEntity> Entities = [];
    public readonly List<IActor> Actors =
    [
        player,
        new CasterSkeleton() { Position = new(100, 100) },
        new CasterSkeleton() { Position = new(300, 100) },
        new CasterSkeleton() { Position = new(500, 100) },
    ];

    private MonsterSpawner _monsterSpawner = new(player, 1d, offscreenDistance: 100);

    public void Update(GameTime gameTime)
    {
        _monsterSpawner.Update(gameTime);

        for (int i = Actors.Count - 1; i >= 0; i--)
        {
            IActor actor = Actors[i];
            actor.Update(gameTime);
        }

        for (int i = Entities.Count - 1; i >= 0; i--)
        {
            IEntity entity = Entities[i];
            entity.Update(gameTime);
        }

        for (int i = 0; i < Items.Count; i++)
        {
            DroppedItem item = Items[i];
            item.Update(gameTime);
        }
    }

    public void RemoveEntity(IEntity entity)
    {
        int index = Entities.FindIndex(e => e.Id == entity.Id);
        Entities.RemoveAt(index);
    }

    public void RemoveActor(IActor actor)
    {
        int index = Actors.FindIndex(e => e.Id == actor.Id);
        Actors.RemoveAt(index);
    }

    public bool OnClick()
    {
        const double itemPickupRadius = 40;

        foreach (DroppedItem item in Items)
        {
            if (item.IsHovered)
            {
                if (Vector2.Distance(Player.Position, item.Position) > itemPickupRadius)
                {
                    int j = (int)item.Position.X;
                    int k = (int)item.Position.Y;
                    double angle = Math.Atan2(
                        player.Position.Y - item.Position.Y,
                        player.Position.X - item.Position.X
                    );
                    double x = itemPickupRadius * Math.Cos(angle) + j;
                    double y = itemPickupRadius * Math.Sin(angle) + k;
                    Vector2 nearestPoint = new((float)x, (float)y);
                    Player.InputComponent.StartMove(nearestPoint);
                    // TODO: pick up the item once reached the radius (unless player moved elsewhere)
                }
                else
                {
                    // pick up item immediately
                    bool addedToInventory = item.GetPickedUp(Player);
                    if (addedToInventory)
                    {
                        Items.Remove(item);
                        return true;
                    }
                }

                return true;
            }
        }
        return false;
    }
}
