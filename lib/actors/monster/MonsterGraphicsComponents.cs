using System;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MonsterGraphicsComponent
{
    private Asset _idleAsset = Assets.Monsters.Skeleton.Idle;
    private Asset _walkAsset = Assets.Monsters.Skeleton.Walk;
    private Asset _deathAsset = Assets.Monsters.Skeleton.Death; // TODO: add one of the two corpse frames
    private readonly float _frameTime = 0.15f;
    private int _currentFrame = 0;
    private float _elapsedTime = 0f;

    public void Update(Monster monster, GameTime gameTime)
    {
        _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime >= _frameTime)
        {
            _elapsedTime = 0f;

            if (monster.State == ActorState.Idling)
            {
                _currentFrame++;
                if (_currentFrame >= _idleAsset.Frames.Count)
                {
                    _currentFrame = 0;
                }
            }
            else if (monster.State == ActorState.Walking)
            {
                _currentFrame++;
                if (_currentFrame >= _walkAsset.Frames.Count)
                {
                    _currentFrame = 0;
                }
            }
            else if (monster.State == ActorState.Dead)
            {
                if (_currentFrame != _deathAsset.Frames.Count - 1)
                    _currentFrame++;
            }
            else
            {
                throw new SystemException("Unhandled ActorState");
            }
        }
    }

    public void Draw(
        Monster monster,
        SpriteBatch spriteBatch,
        GraphicsDevice device,
        bool showHitbox = false
    )
    {
        Asset asset;
        switch (monster.State)
        {
            case ActorState.Idling:
            {
                asset = _idleAsset;
                break;
            }
            case ActorState.Walking:
            {
                asset = _walkAsset;
                break;
            }
            case ActorState.Dead:
            {
                asset = _deathAsset;
                break;
            }
            default:
                throw new SystemException("Unhandled ActorState");
        }

        var effect =
            monster.Facing == ActorFacing.Right
                ? SpriteEffects.None
                : SpriteEffects.FlipHorizontally;

        if (showHitbox)
        {
            var rectangleTexture = new Texture2D(device, 1, 1);
            rectangleTexture.SetData([Color.Yellow]);
            spriteBatch.Draw(
                rectangleTexture,
                monster.Hitbox,
                null,
                Color.Yellow,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                Layer.Hitbox
            );
        }

        spriteBatch.Draw(
            asset.Texture,
            new((int)monster.Position.X, (int)monster.Position.Y),
            asset.Frames[_currentFrame],
            Color.White,
            0f,
            new Vector2(asset.Texture.Width / asset.Frames.Count / 2, asset.Texture.Height / 2),
            1f,
            effect,
            Layer.Monster
        );

        string monsterHealth = $"{monster.Health}";
        Vector2 monsterHealthOrigin = Assets.Fonts.MonogramExtened.MeasureString(monsterHealth);

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            monsterHealth,
            new((int)monster.Position.X, (int)monster.Position.Y + 30),
            Color.White,
            0f,
            new((int)(monsterHealthOrigin.X / 2), (int)(monsterHealthOrigin.Y / 2)),
            1f,
            SpriteEffects.None,
            Layer.Text
        );
    }

    public void ResetFrames()
    {
        _currentFrame = 0;
    }
}
