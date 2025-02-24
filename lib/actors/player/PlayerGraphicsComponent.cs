using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PlayerGraphicsComponent
{
    private Player _player;
    private Asset _idleAsset = Assets.Player.Idle;
    private Asset _walkAsset = Assets.Player.Walk;
    private Asset _currentAsset = Assets.Player.Idle;
    private int _currentFrame = 0;
    private float _frameTime = 0.1f;
    private float _elapsedTime = 0f;

    public PlayerGraphicsComponent(Player player)
    {
        _player = player;
    }

    public void Update(GameTime gameTime)
    {
        _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime >= _frameTime)
        {
            _elapsedTime = 0f;

            _currentAsset = (_player.State, _player.ActionState) switch
            {
                (_, ActorActionState.Casting) => _idleAsset,
                (ActorState.Idling, _) => _idleAsset,
                (ActorState.Walking, _) => _walkAsset,
                _ => throw new SystemException("Unhandled ActorState"),
            };

            _currentFrame = (_currentFrame + 1) % _currentAsset.Frames.Count;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var texture = _currentAsset.Texture;
        var frame = _currentAsset.Frames[_currentFrame];
        var effect =
            _player.Facing == ActorFacing.Right
                ? SpriteEffects.None
                : SpriteEffects.FlipHorizontally;

        if (GameState.IsDebugMode)
        {
            if (_player.Hitbox is RectangleHitbox rectangleHitbox)
            {
                spriteBatch.Draw(
                    Assets.RectangleTexture,
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
            new((int)_player.Position.X, (int)_player.Position.Y),
            frame,
            Color.White,
            0f,
            new Vector2(frame.Width / 2, frame.Height / 2),
            1f,
            effect,
            Layer.Player
        );
    }

    public void ResetFrames()
    {
        _currentFrame = 0;
    }
}
