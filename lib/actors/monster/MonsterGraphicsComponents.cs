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
            spriteBatch.Draw(rectangleTexture, monster.Hitbox, Color.Yellow);
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
            0f
        );

        // Drawing text
        // Szkeletor health
        float textScale = 1.0f;
        float layerdepth = 1.0f;
        float rotation = 0.0f;

        string monsterHealth = $"{monster.Health}";
        Vector2 monsterHealthOrigin = Assets.Fonts.MonogramExtened.MeasureString(monsterHealth);

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            monsterHealth,
            new Vector2(monster.Position.X, monster.Position.Y + 50),
            Color.Black,
            rotation,
            monsterHealthOrigin / 2,
            textScale,
            SpriteEffects.None,
            layerdepth
        );

        // int textureWidth = _idleTextures[0].Width;
        // int textureHeight = _idleTextures[0].Height;
        // spriteBatch.DrawString(
        //     Game1.font,
        //     $"{textureWidth} {textureHeight}",
        //     new Vector2(0, 32),
        //     Color.Black, rotation, Vector2.Zero, textScale, SpriteEffects.None, layerdepth
        // );
    }

    public void ResetFrames()
    {
        _currentFrame = 0;
    }
}
