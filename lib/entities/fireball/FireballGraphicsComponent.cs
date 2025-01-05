using System.Collections.Generic;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FireballGraphicsComponent
{
    private List<Texture2D> _textures = Assets.Spells.Fireball;
    private int _currentFrame = 0;

    public void Draw(Fireball fireball, SpriteBatch spriteBatch, GraphicsDevice device)
    {
        Texture2D texture = _textures[_currentFrame];
        _currentFrame++;
        if (_currentFrame >= _textures.Count)
            _currentFrame = 0;

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
            texture,
            new((int)fireball.Position.X, (int)fireball.Position.Y),
            null,
            Color.White,
            (float)fireball.Angle,
            new Vector2(texture.Width / 2, texture.Height / 2),
            1f,
            SpriteEffects.None,
            Layer.Entity
        );
    }
}
