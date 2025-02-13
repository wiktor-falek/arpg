using System.Collections.Generic;
using arpg;
using Microsoft.Xna.Framework;

public class World(Player player)
{
    // register World.OnClick -> iterate over dropped items that are hovered -> add item to player inventory
    public readonly Player Player = player;
    public readonly List<DroppedItem> Items =
    [
        new DroppedItem(new Sandals(), Vector2.Zero),
        new DroppedItem(
            new Item("Mirror of Kalandra", Rarity.Normal, 1, 1, Assets.Items.None_1x1),
            new(100, 100)
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
}
