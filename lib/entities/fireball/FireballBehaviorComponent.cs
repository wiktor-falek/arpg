using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class FireballBehaviorComponent
{
    private List<string> _hitActors = [];
    public float CurrentDuration = 0f;

    public void Update(Fireball fireball, GameTime gameTime)
    {
        var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        CurrentDuration += elapsedTime;

        if (CurrentDuration >= fireball.MaxDuration)
        {
            GameState.RemoveEntity(fireball);
            return;
        }

        double x = fireball.Position.X + (fireball.Speed * elapsedTime * Math.Cos(fireball.Angle));
        double y = fireball.Position.Y + (fireball.Speed * elapsedTime * Math.Sin(fireball.Angle));
        fireball.Position = new((float)x, (float)y);

        foreach (var actor in GameState.Actors)
        {
            if (
                actor is Monster
                && !_hitActors.Contains(actor.Id)
                && fireball.Hitbox.Intersects(actor.Hitbox)
            )
            {
                actor.TakeDamage(fireball.Damage);
                _hitActors.Add(actor.Id);
            }
        }
    }
}
