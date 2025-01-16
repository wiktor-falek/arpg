using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HolyFireGraphicsComponent
{
    public HolyFireGraphicsComponent() { }

    public void Draw(HolyFireEntity holyFire, SpriteBatch spriteBatch, GraphicsDevice device)
    {
        Texture2D circleTexture = CreateCircleTexture(device, (int)holyFire.Radius);
        spriteBatch.Draw(
            circleTexture,
            new(
                (int)(holyFire.Position.X - holyFire.Radius),
                (int)(holyFire.Position.Y - holyFire.Radius)
            ),
            null,
            new Color(205, 45, 10, 64),
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.PlayerOnGroundEffect
        );
    }

    Texture2D CreateCircleTexture(GraphicsDevice device, int radius)
    {
        int diameter = radius * 2;
        Texture2D texture = new Texture2D(device, diameter, diameter);
        Color[] colorData = new Color[diameter * diameter];

        float radiusSquared = radius * radius;

        for (int x = 0; x < diameter; x++)
        {
            for (int y = 0; y < diameter; y++)
            {
                int index = x + y * diameter;
                Vector2 pos = new(x - radius, y - radius);
                if (pos.LengthSquared() <= radiusSquared)
                {
                    colorData[index] = Color.White;
                }
                else
                {
                    colorData[index] = Color.Transparent;
                }
            }
        }

        texture.SetData(colorData);
        return texture;
    }
}
