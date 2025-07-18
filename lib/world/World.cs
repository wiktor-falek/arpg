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

        // Items.Add(new DroppedItem(new AugmentingCore(), new(0, 0)));
        // Items.Add(new DroppedItem(new Hood(), new(50, 0)));
        // Items.Add(new DroppedItem(new Sandals(), new(0, 50)));
        // Items.Add(new DroppedItem(new SapphireRing().ToRare(), new(0, 100)));
        // Items.Add(new DroppedItem(new RubyRing(), new(0, 200)));

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

        for (int i = Items.Count - 1; i >= 0; i--)
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

    public bool OnLeftClick()
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
                item.GetPickedUp(Player); // TODO: player.PickUpItem(item)?
            }

            return true;
        }

        return false;
    }
}
