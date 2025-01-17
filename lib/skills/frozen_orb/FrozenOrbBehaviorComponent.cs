using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class FrozenOrbBehaviorComponent
{
    public float CurrentDuration = 0f;
    private List<FrozenOrbSecondaryEntity> SecondaryEntities = [];
    private float _frameTime = 0f;
    private float _secondaryProjectileInterval = 0.15f;
    private float _rotationSpeed = 15f;

    public void Update(FrozenOrbEntity frozenOrb, GameTime gameTime)
    {
        var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _frameTime += elapsedTime;
        CurrentDuration += elapsedTime;

        if (CurrentDuration >= frozenOrb.MaxDuration)
        {
            GameState.RemoveEntity(frozenOrb);
            return;
        }

        double x =
            frozenOrb.Position.X + (frozenOrb.Speed * elapsedTime * Math.Cos(frozenOrb.Angle));
        double y =
            frozenOrb.Position.Y + (frozenOrb.Speed * elapsedTime * Math.Sin(frozenOrb.Angle));
        frozenOrb.Position = new((float)x, (float)y);

        // TODO: review
        frozenOrb.Rotation = (frozenOrb.Rotation + _rotationSpeed * elapsedTime) % 360;
        if (frozenOrb.Rotation < 0)
        {
            frozenOrb.Rotation += 360;
        }
        frozenOrb.Rotation += 1;

        if (_frameTime >= _secondaryProjectileInterval)
        {
            FrozenOrbSecondaryEntity secondaryEntity = new()
            {
                Position = new(frozenOrb.Position.X, frozenOrb.Position.Y),
                Angle = frozenOrb.Rotation,
            };
            SecondaryEntities.Add(secondaryEntity);
            GameState.Entities.Add(secondaryEntity);
            _frameTime -= _secondaryProjectileInterval;
        }
    }
}
