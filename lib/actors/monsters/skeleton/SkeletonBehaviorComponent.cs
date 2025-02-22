using System;
using arpg;
using Microsoft.Xna.Framework;

/*
    TODO: replace logic with actions
*/

public class SkeletonBehaviorComponent
{
    private float _corpseDespawnTime = 10f;
    private bool _playerWasHit = false;
    private bool _isAttacking = false;
    private float _timeSinceDeath = 0f;
    private float _attackInterval = 0.9f;
    private double _swingTimer = 0.3f;
    private const float _ATTACK_LAND_FRAME = 0.6f;

    public void Update(Skeleton monster, GameTime time)
    {
        double elapsedTime = time.ElapsedGameTime.TotalSeconds;

        if (monster.State == ActorState.Dead)
        {
            _timeSinceDeath += (float)elapsedTime;

            if (_timeSinceDeath >= _corpseDespawnTime)
            {
                Game1.World.RemoveActor(monster);
            }

            monster.ActionState = ActorActionState.None;
            return;
        }

        Vector2 playerPosition = Game1.World.Player.Position;

        // TODO: ActorLeashAction?
        double distance = Vector2.Distance(monster.Position, playerPosition);
        if (distance <= 100)
        {
            monster.IsLeashed = true;
        }
        else
        {
            monster.IsLeashed = false;
        }

        if (monster.IsLeashed)
        {
            // TODO: update existing
            monster.Action = new ActorMoveAction(monster, playerPosition);
        }
        else
        {
            monster.TransitionState(ActorState.Idling);
            monster.Action = null;
        }

        // if (!monster.IsLeashed)
        // {
        //     monster.TransitionState(ActorState.Idling);
        //     return;
        // }

        // float deltaX = x2 - x1;
        // float deltaY = y2 - y1;
        // double angle = Math.Atan2(deltaY, deltaX);

        // double x = monster.Position.X + (monster.Stats.Speed * elapsedTime * Math.Cos(angle));
        // double y = monster.Position.Y + (monster.Stats.Speed * elapsedTime * Math.Sin(angle));

        // bool withinAttackHitDistance = distance <= 64;
        // bool withinAttackTriggerDistance = distance <= 32;

        // if (withinAttackTriggerDistance && !_isAttacking)
        // {
        //     monster.TransitionState(ActorState.Idling);
        //     monster.TransitionState(ActorActionState.Swinging);
        //     _isAttacking = true;
        // }

        // if (_isAttacking)
        // {
        //     _swingTimer += elapsedTime;

        //     if (_swingTimer >= _ATTACK_LAND_FRAME)
        //     {
        //         // TODO: there should be an attack duration i.e. how long can you get hit for after starting swinging
        //         if (withinAttackHitDistance && !_playerWasHit)
        //         {
        //             // TODO: proper hitbox checking
        //             Game1.World.Player.TakeDamage(10);
        //             _playerWasHit = true;
        //         }
        //     }

        //     if (_swingTimer >= _attackInterval)
        //     {
        //         _swingTimer = 0f;
        //         _isAttacking = false;
        //         _playerWasHit = false;
        //     }
        // }
        // else
        // {
        //     monster.Position = new((float)x, (float)y);
        //     monster.TransitionState(ActorState.Walking);
        //     monster.TransitionState(ActorActionState.None);
        // }
    }
}
