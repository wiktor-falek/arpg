using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using arpg;
using Microsoft.Xna.Framework;

public class World
{
    public readonly Player Player;
    public readonly List<DroppedItem> Items = [];
    public readonly List<IEntity> Entities = [];
    public readonly List<IActor> Actors = [];
    private MonsterSpawner _monsterSpawner;

    public World(Player player)
    {
        Player = player;
        _monsterSpawner = new MonsterSpawner(Player, 0.5d, offscreenDistance: 100);

        Items.Add(new DroppedItem(new Hood(), new(100, 100)));
        Items.Add(new DroppedItem(new Sandals(), new(100, 200)));
        Items.Add(
            new DroppedItem(
                new Item("Mirror of Kalandra", Rarity.Normal, 1, 1, Assets.Items.None_1x1),
                new(0, 0)
            )
        );

        Actors.Add(Player);
    }

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

        foreach (DroppedItem item in Items.Where(item => item.IsHovered))
        {
            if (Vector2.Distance(Player.Position, item.Position) > itemPickupRadius)
            {
                double angle = Math.Atan2(
                    Player.Position.Y - item.Position.Y,
                    Player.Position.X - item.Position.X
                );
                Vector2 pickupPoint = new(
                    (float)((itemPickupRadius - 1) * Math.Cos(angle) + item.Position.X),
                    (float)((itemPickupRadius - 1) * Math.Sin(angle) + item.Position.Y)
                );
                Player.InputComponent.StartMove(pickupPoint);
                // TODO: pick up the item once reached the radius (unless player moved elsewhere)
            }
            else
            {
                // pick up item immediately
                bool addedToInventory = item.GetPickedUp(Player);
                if (addedToInventory)
                {
                    Items.Remove(item);
                }
            }

            return true;
        }

        return false;
    }
}
