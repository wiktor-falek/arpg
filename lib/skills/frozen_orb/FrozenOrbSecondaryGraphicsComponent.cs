using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class FrozenOrbSecondaryGraphicsComponent
{
    private Asset _asset = Assets.Spells.FrozenOrbSecondary;

    public void Draw(
        FrozenOrbSecondaryEntity frozenOrbSecondary,
        SpriteBatch spriteBatch,
        GraphicsDevice device
    )
    {
        if (GameState.IsDebugMode)
        {
            if (frozenOrbSecondary.Hitbox is RectangleHitbox rectangleHitbox)
            {
                var rectangleTexture = new Texture2D(device, 1, 1);
                rectangleTexture.SetData([Color.Yellow]);
                spriteBatch.Draw(
                    rectangleTexture,
                    rectangleHitbox.Bounds,
                    null,
                    Color.Yellow,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    Layer.Hitbox
                );
            }
            else
            {
                throw new NotImplementedException("Unhandled hitbox type");
            }
        }

        spriteBatch.Draw(
            _asset.Texture,
            new((int)frozenOrbSecondary.Position.X, (int)frozenOrbSecondary.Position.Y),
            _asset.Frames[0],
            Color.White,
            (float)frozenOrbSecondary.Angle + MathHelper.ToRadians(90),
            new Vector2(_asset.Texture.Width / _asset.Frames.Count / 2, _asset.Texture.Height / 2),
            1f,
            SpriteEffects.None,
            Layer.Entity
        );
    }
}
