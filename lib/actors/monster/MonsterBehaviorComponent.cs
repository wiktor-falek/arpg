using System;
using Microsoft.Xna.Framework;

public class MonsterBehaviorComponent
{
    private float _corpseDespawnTime = 10f;
    private bool _attackStarted = false;
    private float _timeSinceDeath = 0f;
    private float _attackInterval = 0.9f;
    private double _swingTimer = 0.3f;

    public void Update(Monster monster, GameTime time)
    {
        double elapsedTime = time.ElapsedGameTime.TotalSeconds;

        if (monster.State == ActorState.Dead)
        {
            _timeSinceDeath += (float)elapsedTime;

            if (_timeSinceDeath >= _corpseDespawnTime)
            {
                GameState.RemoveActor(monster);
            }
            return;
        }

        float x1 = monster.Position.X;
        float y1 = monster.Position.Y;
        float x2 = GameState.Player.Position.X;
        float y2 = GameState.Player.Position.Y;

        if (x1 < x2)
        {
            monster.Facing = ActorFacing.Right;
        }
        else if (x1 > x2)
        {
            monster.Facing = ActorFacing.Left;
        }

        double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

        if (distance <= 150)
        {
            monster.IsLeashed = true;
        }

        if (!monster.IsLeashed)
        {
            monster.TransitionState(ActorState.Idling);
            return;
        }

        float deltaX = x2 - x1;
        float deltaY = y2 - y1;
        double angle = Math.Atan2(deltaY, deltaX);

        double x = monster.Position.X + (monster.Speed * elapsedTime * Math.Cos(angle));
        double y = monster.Position.Y + (monster.Speed * elapsedTime * Math.Sin(angle));

        bool withinAttackHitDistance = distance <= 64;
        bool withinAttackTriggerDistance = distance <= 32;

        if (withinAttackTriggerDistance && !_attackStarted)
        {
            monster.TransitionState(ActorState.Idling);
            monster.TransitionState(ActorActionState.Swinging);
            _attackStarted = true;
        }

        if (_attackStarted)
        {
            _swingTimer += elapsedTime;
            if (_swingTimer >= _attackInterval)
            {
                _swingTimer = 0f;
                _attackStarted = false;

                if (withinAttackHitDistance)
                    GameState.Player.TakeDamage(10);
            }
        }
        else
        {
            monster.Position = new((float)x, (float)y);
            monster.TransitionState(ActorState.Walking);
            monster.TransitionState(ActorActionState.None);
        }
    }
}
