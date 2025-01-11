using System.Collections.Generic;

public static class GameState
{
    public static bool IsDebugMode = false;
    public static readonly Player Player = new() { Position = new(100, 100) };
    public static readonly List<IEntity> Entities = [];
    public static readonly List<IActor> Actors =
    [
        new Monster() { Position = new(401, 200 + 0) },
        new Monster() { Position = new(401, 200 + 100) },
        new Monster() { Position = new(401, 200 + 200) },
    ];

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
