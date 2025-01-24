using System;
using Microsoft.Xna.Framework;

public class CasterSkeletonBehaviorComponent
{
    private float _corpseDespawnTime = 10f;
    private bool _playerWasHit = false;
    private bool _isAttacking = false;
    private float _timeSinceDeath = 0f;
    private float _attackInterval = 0.9f;
    private double _castTimer = 0.3f;
    private const float _ATTACK_LAND_FRAME = 0.6f;

    public void Update(CasterSkeleton monster, GameTime time)
    {
        double elapsedTime = time.ElapsedGameTime.TotalSeconds;

        // TODO: reuse
        if (monster.State == ActorState.Dead)
        {
            _timeSinceDeath += (float)elapsedTime;

            if (_timeSinceDeath >= _corpseDespawnTime)
            {
                GameState.RemoveActor(monster);
            }

            monster.ActionState = ActorActionState.None;
            return;
        }

        float x1 = monster.Position.X;
        float y1 = monster.Position.Y;
        float x2 = GameState.Player.Position.X;
        float y2 = GameState.Player.Position.Y;
        float deltaX = x2 - x1;
        float deltaY = y2 - y1;
        double angle = Math.Atan2(deltaY, deltaX);

        // TODO: reuse???
        if (x1 < x2)
        {
            monster.Facing = ActorFacing.Right;
        }
        else if (x1 > x2)
        {
            monster.Facing = ActorFacing.Left;
        }

        double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

        if (distance <= 200)
        {
            monster.IsLeashed = true;
        }

        if (!monster.IsLeashed)
        {
            monster.TransitionState(ActorState.Idling);
            return;
        }

        // is leashed

        if (distance > 200)
        {
            // follow player
            double x = monster.Position.X + (monster.Speed * elapsedTime * Math.Cos(angle));
            double y = monster.Position.Y + (monster.Speed * elapsedTime * Math.Sin(angle));

            monster.Position = new((float)x, (float)y);
        }
        else
        {
            // start casting
            FireballEntity fireballEntity = new(monster)
            {
                Position = new(monster.Position.X, monster.Position.Y),
                Angle = angle,
            };
            GameState.Entities.Add(fireballEntity);
        }

        //     bool withinAttackHitDistance = distance <= 64;
        //     bool withinAttackTriggerDistance = distance <= 32;

        //     if (withinAttackTriggerDistance && !_isAttacking)
        //     {
        //         monster.TransitionState(ActorState.Idling);
        //         monster.TransitionState(ActorActionState.Swinging);
        //         _isAttacking = true;
        //     }

        //     if (_isAttacking)
        //     {
        //         _castTimer += elapsedTime;

        //         if (_castTimer >= _ATTACK_LAND_FRAME)
        //         {
        //             // TODO: there should be an attack duration i.e. how long can you get hit for after starting swinging
        //             if (withinAttackHitDistance && !_playerWasHit)
        //             {
        //                 // TODO: proper hitbox checking
        //                 GameState.Player.TakeDamage(10);
        //                 _playerWasHit = true;
        //             }
        //         }

        //         if (_castTimer >= _attackInterval)
        //         {
        //             _castTimer = 0f;
        //             _isAttacking = false;
        //             _playerWasHit = false;
        //         }
        //     }
        //     else
        //     {
        //         monster.Position = new((float)x, (float)y);
        //         monster.TransitionState(ActorState.Walking);
        //         monster.TransitionState(ActorActionState.None);
        //     }
    }
}
