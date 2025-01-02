using System;
using System.Collections.Generic;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PlayerGraphicsComponent
{
    private Texture2D _idleTexture = Assets.GetTextures("player/player_idle");
    private Texture2D _walkTexture = Assets.GetTextures("player/player_walk");
    private List<Rectangle> _idleFrames = [];
    private List<Rectangle> _walkFrames = [];

    private int _currentFrame = 0;
    private float _frameTime = 0.1f;
    private float _elapsedTime = 0f;

    public PlayerGraphicsComponent()
    {
        for (int i = 0; i < 10; i++)
        {
            _idleFrames.Add(new Rectangle(140 * i, 0, 140, 140));
        }

        for (int i = 0; i < 8; i++)
        {
            _walkFrames.Add(new Rectangle(140 * i, 0, 140, 140));
        }
    }

    public void Update(Player player, GameTime gameTime)
    {
        _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime >= _frameTime)
        {
            _elapsedTime = 0f;

            _currentFrame++;
            if (player.State == ActorState.Idling)
            {
                if (_currentFrame >= _idleFrames.Count)
                {
                    _currentFrame = 0;
                }
            }
            else if (player.State == ActorState.Walking)
            {
                if (_currentFrame >= _walkFrames.Count)
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
        var texture = player.State == ActorState.Idling ? _idleTexture : _walkTexture;
        var frame =
            player.State == ActorState.Idling
                ? _idleFrames[_currentFrame]
                : _walkFrames[_currentFrame];
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
