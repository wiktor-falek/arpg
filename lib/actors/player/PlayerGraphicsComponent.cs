using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PlayerGraphicsComponent
{
    private Asset _idleAsset = Assets.Player.Idle;
    private Asset _walkAsset = Assets.Player.Walk;
    private int _currentFrame = 0;
    private float _frameTime = 0.1f;
    private float _elapsedTime = 0f;

    public void Update(Player player, GameTime gameTime)
    {
        _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime >= _frameTime)
        {
            _elapsedTime = 0f;

            _currentFrame++;
            if (player.State == ActorState.Idling)
            {
                if (_currentFrame >= _idleAsset.Frames.Count)
                {
                    _currentFrame = 0;
                }
            }
            else if (player.State == ActorState.Walking)
            {
                if (_currentFrame >= _walkAsset.Frames.Count)
                {
                    _currentFrame = 0;
                }
            }
            else
            {
                throw new SystemException("Unhandled ActorState");
            }
        }
    }

    public void Draw(Player player, SpriteBatch spriteBatch, GraphicsDevice device)
    {
        var texture = player.State == ActorState.Idling ? _idleAsset.Texture : _walkAsset.Texture;
        var frame =
            player.State == ActorState.Idling
                ? _idleAsset.Frames[_currentFrame]
                : _walkAsset.Frames[_currentFrame];
        var effect =
            player.Facing == ActorFacing.Right
                ? SpriteEffects.None
                : SpriteEffects.FlipHorizontally;

        if (GameState.IsDebugMode)
        {
            if (player.Hitbox is RectangleHitbox rectangleHitbox)
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
            texture,
            new((int)player.Position.X, (int)player.Position.Y),
            frame,
            Color.White,
            0f,
            new Vector2(frame.Width / 2, frame.Height / 2),
            1f,
            effect,
            Layer.Text
        );

        Texture2D circleTexture = CreateCircleTexture(device, (int)player.HolyFire.Radius);

        spriteBatch.Draw(
            circleTexture,
            new((int)player.HolyFire.Position.X, (int)player.HolyFire.Position.Y),
            null,
            new Color(205, 45, 10, 64),
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.PlayerOnGroundEffect
        );
    }

    public void ResetFrames()
    {
        _currentFrame = 0;
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
                Vector2 pos = new Vector2(x - radius, y - radius);
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
