using System;
using System.Collections.Generic;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MonsterGraphicsComponent
{
    private List<Texture2D> _idleTextures = Assets.GetTextures(
        ["monsters/skeleton_ready_1", "monsters/skeleton_ready_2", "monsters/skeleton_ready_3"]
    );
    private List<Texture2D> _walkTextures = [];

    private readonly float _frameTime = 0.25f;
    private int _currentFrame = 0;
    private float _elapsedTime = 0f;

    public void Update(Monster monster, GameTime gameTime)
    {
        _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_elapsedTime >= _frameTime)
        {
            _elapsedTime = 0f;

            _currentFrame++;
            if (monster.State == ActorState.Idling)
            {
                if (_currentFrame >= _idleTextures.Count)
                {
                    _currentFrame = 0;
                }
            }
            else if (monster.State == ActorState.Walking)
            {
                if (_currentFrame >= _walkTextures.Count)
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

    public void Draw(Monster monster, SpriteBatch spriteBatch)
    {
        // @TODO: This has to be updated for non-idle textures
        var texture = _idleTextures[_currentFrame];
        var effect =
            monster.Facing == ActorFacing.Right
                ? SpriteEffects.None
                : SpriteEffects.FlipHorizontally;

        spriteBatch.Draw(
            texture,
            monster.Position,
            null,
            Color.White,
            0f,
            new Vector2(texture.Width / 2, texture.Height / 2),
            2f,
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
}
