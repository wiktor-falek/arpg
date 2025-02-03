using System.Collections.Generic;
using Microsoft.Xna.Framework;

public static class GameState
{
    public static bool IsDebugMode = false;
    public static readonly Player Player = new() { Position = new(0, 0) };
    public static readonly List<IEntity> Entities = [];
    public static readonly List<IActor> Actors =
    [
        Player,
        new CasterSkeleton() { Position = new(100, 100) },
        new CasterSkeleton() { Position = new(300, 100) },
        new CasterSkeleton() { Position = new(500, 100) }
    ];

    private static MonsterSpawner _monsterSpawner = new(Player, 3d, offscreenDistance: 100);

    public static void Update(GameTime gameTime)
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

        Camera.Follow(Player);
    }

    public static void RemoveEntity(IEntity entity)
    {
        int index = Entities.FindIndex(e => e.Id == entity.Id);
        Entities.RemoveAt(index);
    }

    public static void RemoveActor(IActor actor)
    {
        int index = Actors.FindIndex(e => e.Id == actor.Id);
        Actors.RemoveAt(index);
    }
}
