using Microsoft.Xna.Framework;

public class HolyFireBehaviorComponent
{
    private float _tickRate = 0.25f;
    private float _frameTime = 0f;

    public void Update(HolyFireEntity holyFire, GameTime gameTime)
    {
        _frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        holyFire.Position = new(holyFire.Owner.Position.X + 0, holyFire.Owner.Position.Y + 20);
        if (_frameTime >= _tickRate)
        {
            int selfDps = 4;
            int dps = 20;
            holyFire.Owner.TakeDamage(selfDps * _tickRate);
            foreach (var actor in GameState.Actors)
            {
                if (actor is Monster && actor.Hitbox.Intersects(holyFire.Hitbox))
                {
                    actor.TakeDamage(dps * _tickRate);
                }
            }
            _frameTime -= _tickRate;
        }
    }
}
