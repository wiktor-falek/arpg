using System.Collections.Generic;

public class GameState
{
    public static bool IsDebugMode = false;
    public static Player Player = new() { Position = new(100, 100) };
    public static List<IEntity> Entities = [];
    public static List<IActor> Actors =
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
