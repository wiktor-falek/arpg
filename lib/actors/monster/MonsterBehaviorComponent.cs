using System;
using arpg;
using Microsoft.Xna.Framework;

public class MonsterBehaviorComponent
{
    private float _corpseDespawnTime = 10f;
    private float _timeSinceDeath = 0f;

    public void Update(Monster monster, GameTime time)
    {
        if (monster.State == ActorState.Dead)
        {
            double elapsedTime = time.ElapsedGameTime.TotalSeconds;
            _timeSinceDeath += (float)elapsedTime;

            if (_timeSinceDeath >= _corpseDespawnTime)
            {
                Game1.RemoveActor(monster);
            }
            return;
        }

        float x1 = monster.Position.X;
        float y1 = monster.Position.Y;
        float x2 = Game1.Player.Position.X;
        float y2 = Game1.Player.Position.Y;

        if (x1 < x2)
        {
            monster.Facing = ActorFacing.Right;
        }
        else if (x1 > x2)
        {
            monster.Facing = ActorFacing.Left;
        }

        double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        if (distance <= 300)
        {
            float deltaX = x2 - x1;
            float deltaY = y2 - y1;
            double angle = Math.Atan2(deltaY, deltaX);

            var elapsedTime = time.ElapsedGameTime.TotalSeconds;
            double x = monster.Position.X + (monster.Speed * elapsedTime * Math.Cos(angle));
            double y = monster.Position.Y + (monster.Speed * elapsedTime * Math.Sin(angle));
            monster.Position = new((float)x, (float)y);
            monster.TransitionState(ActorState.Walking);
        }
        else
        {
            monster.TransitionState(ActorState.Idling);
        }
    }
}
