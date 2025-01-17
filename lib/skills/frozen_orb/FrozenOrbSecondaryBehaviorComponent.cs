using System;
using Microsoft.Xna.Framework;

public class FrozenOrbSecondaryBehaviorComponent
{
    public float CurrentDuration = 0f;
    private float _speed = 150f;

    public void Update(FrozenOrbSecondaryEntity secondaryEntity, GameTime gameTime)
    {
        var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        CurrentDuration += elapsedTime;

        if (CurrentDuration >= secondaryEntity.MaxDuration)
        {
            GameState.RemoveEntity(secondaryEntity);
            return;
        }

        double x =
            secondaryEntity.Position.X + (_speed * elapsedTime * Math.Cos(secondaryEntity.Angle));
        double y =
            secondaryEntity.Position.Y + (_speed * elapsedTime * Math.Sin(secondaryEntity.Angle));
        secondaryEntity.Position = new((float)x, (float)y);
    }
}
