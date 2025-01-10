using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Background
{
    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                float tileX = ((int)GameState.Player.Position.X / 64 + i) * 64 - 64 * 2;
                float tileY = ((int)GameState.Player.Position.Y / 64 + j) * 64 - 64 * 2;
                spriteBatch.Draw(
                    Assets.Environment.Cobblestone.Texture,
                    new(tileX - GameState.Player.Position.X, tileY - GameState.Player.Position.Y),
                    Assets.Environment.Cobblestone.Frames[0],
                    new Color(90, 90, 90),
                    0f,
                    Vector2.Zero,
                    0.0625f,
                    SpriteEffects.None,
                    Layer.Background
                );
            }
        }
    }
}
