using System;
using System.Collections.Generic;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MonsterGraphicsComponent
{
    private List<Texture2D> _idleTextures = Assets.Monsters.Skeleton.Idle;
    private List<Texture2D> _walkTextures = Assets.Monsters.Skeleton.Walk;
    private List<Texture2D> _deathTextures = new(Assets.Monsters.Skeleton.Death)
    {
        Assets.Monsters.Skeleton.Corpse[0],
    };

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
                if (_currentFrame >= _idleTextures.Count)
                {
                    _currentFrame = 0;
                }
            }
            else if (monster.State == ActorState.Walking)
            {
                _currentFrame++;
                if (_currentFrame >= _walkTextures.Count)
                {
                    _currentFrame = 0;
                }
            }
            else if (monster.State == ActorState.Dead)
            {
                if (_currentFrame != _deathTextures.Count - 1)
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
        // @TODO: This has to be updated for non-idle textures
        Texture2D texture;
        switch (monster.State)
        {
            case ActorState.Idling:
            {
                texture = _idleTextures[_currentFrame];
                break;
            }
            case ActorState.Walking:
            {
                texture = _walkTextures[_currentFrame];
                break;
            }
            case ActorState.Dead:
            {
                texture = _deathTextures[_currentFrame];
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
            texture,
            new((int)monster.Position.X, (int)monster.Position.Y),
            null,
            Color.White,
            0f,
            new Vector2(texture.Width / 2, texture.Height / 2),
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
            Color.Black,
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
