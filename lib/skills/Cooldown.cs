using System;
using Microsoft.Xna.Framework;

public class Cooldown(double duration)
{
    public double Duration = duration;
    public double Remaining = 0;

    public void Update(GameTime gameTime)
    {
        if (Remaining > 0)
        {
            Remaining -= Math.Max(gameTime.ElapsedGameTime.TotalSeconds, 0);
        }
    }

    public void StartCooldown()
    {
        Remaining = Duration;
    }

    public bool CanCast() => Remaining <= 0;
}
