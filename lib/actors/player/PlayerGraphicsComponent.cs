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

    public void Draw(
        Player player,
        SpriteBatch spriteBatch,
        GraphicsDevice device,
        bool showHitbox = false
    )
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

        if (showHitbox)
        {
            var rectangleTexture = new Texture2D(device, 1, 1);
            rectangleTexture.SetData([Color.Yellow]);
            spriteBatch.Draw(rectangleTexture, player.Hitbox, Color.Yellow);
        }

        spriteBatch.Draw(
            texture,
            player.Position,
            frame,
            Color.White,
            0f,
            new Vector2(frame.Width / 2, frame.Height / 2),
            2.0f,
            effect,
            0f
        );
    }

    public void ResetFrames()
    {
        _currentFrame = 0;
    }
}
