using Microsoft.Xna.Framework;

public interface IActorAction
{
    void Update(GameTime gameTime);
}


/*
    All of these cancel previous action, but it might not be the case in the future.
    Then the actions will be stored in a List, and each action will mutate the actions array.

    Move - moving towards a point on the map
    Cast - casting a skill, stops movement
    Loot - moving towards an item, and picking it up at the end, interrupted by any other action
*/
