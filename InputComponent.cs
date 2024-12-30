using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputComponent
{
    public void Update(Player player, GameTime gameTime)
    {
        var kstate = Keyboard.GetState();

        Vector2 movementDirection = Vector2.Zero;

        if (kstate.IsKeyDown(Keys.W))
        {
            movementDirection.Y -= 1;
        }
        if (kstate.IsKeyDown(Keys.S))
        {
            movementDirection.Y += 1;
        }
        if (kstate.IsKeyDown(Keys.A))
        {
            movementDirection.X -= 1;
        }
        if (kstate.IsKeyDown(Keys.D))
        {
            movementDirection.X += 1;
        }

        if (movementDirection.X == 1)
        {
            player.facing = PlayerFacing.Right;
        }
        else if (movementDirection.X == -1)
        {
            player.facing = PlayerFacing.Left;
        }

        if (movementDirection.Length() > 0)
        {
            movementDirection.Normalize();
            player.TransitionState(PlayerState.Walking);
        }
        else
        {
            player.TransitionState(PlayerState.Idling);
        }

        float updatedSpeed = player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        player.Position += movementDirection * updatedSpeed;
    }
}
