using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class PlayerInputComponent
{
    public void Update(Player player, GameTime gameTime)
    {
        var kstate = Keyboard.GetState();

        Vector2 movementDirection = Vector2.Zero;

        if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up))
        {
            movementDirection.Y -= 1;
        }

        if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down))
        {
            movementDirection.Y += 1;
        }

        if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
        {
            movementDirection.X -= 1;
        }

        if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
        {
            movementDirection.X += 1;
        }

        if (movementDirection.X == 1)
        {
            player.Facing = ActorFacing.Right;
        }
        else if (movementDirection.X == -1)
        {
            player.Facing = ActorFacing.Left;
        }

        if (movementDirection.Length() > 0)
        {
            movementDirection.Normalize();
            player.TransitionState(ActorState.Walking);
        }
        else
        {
            player.TransitionState(ActorState.Idling);
        }

        float updatedSpeed = player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        player.Position += movementDirection * updatedSpeed;
    }
}
