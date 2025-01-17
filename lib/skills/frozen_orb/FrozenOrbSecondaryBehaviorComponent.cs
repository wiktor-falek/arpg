using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

public class FrozenOrbSecondaryBehaviorComponent
{
    public float CurrentDuration = 0f;
    private List<string> _hitActors = [];
    private float _speed = 50f;

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

        foreach (IActor actor in GameState.Actors.Where(actor => actor is Monster))
        {
            if (!_hitActors.Contains(actor.Id) && secondaryEntity.Hitbox.Intersects(actor.Hitbox))
            {
                actor.TakeDamage(secondaryEntity.Damage);
                _hitActors.Add(actor.Id);
            }
        }
    }
}
