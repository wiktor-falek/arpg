using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FireballGraphicsComponent
{
    private Asset _asset = Assets.Spells.Fireball;
    private int _currentFrame = 0;

    public void Draw(
        Fireball fireball,
        SpriteBatch spriteBatch,
        GraphicsDevice device,
        bool showHitbox = false
    )
    {
        _currentFrame++;
        if (_currentFrame >= _asset.Frames.Count)
            _currentFrame = 0;

        if (showHitbox)
        {
            var rectangleTexture = new Texture2D(device, 1, 1);
            rectangleTexture.SetData([Color.Yellow]);
            spriteBatch.Draw(
                rectangleTexture,
                fireball.Hitbox,
                null,
                Color.Yellow,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                Layer.Hitbox
            );
        }

        spriteBatch.Draw(
            _asset.Texture,
            new((int)fireball.Position.X, (int)fireball.Position.Y),
            _asset.Frames[_currentFrame],
            Color.White,
            (float)fireball.Angle,
            new Vector2(_asset.Texture.Width / _asset.Frames.Count / 2, _asset.Texture.Height / 2),
            1f,
            SpriteEffects.None,
            Layer.Entity
        );
    }
}
