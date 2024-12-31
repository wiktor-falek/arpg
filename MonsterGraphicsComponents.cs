using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class MonsterGraphicsComponent
{
    private List<Texture2D> _idleTextures = [];

    // private List<Texture2D> WalkTextures = [];

    private readonly float _frameTime = 0.25f;
    private int _currentFrame = 0;
    private float _elapsedTime = 0f;

    public void LoadAssets(ContentManager content)
    {
        _idleTextures.Add(content.Load<Texture2D>("skeleton_ready_1"));
        _idleTextures.Add(content.Load<Texture2D>("skeleton_ready_2"));
        _idleTextures.Add(content.Load<Texture2D>("skeleton_ready_3"));
    }

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
            // else if (monster.State == ActorState.Idling)
            // {
            //     if (_currentFrame >= IdleFrames.Count)
            //     {
            //         _currentFrame = 0;
            //     }
            // }
            // else
            // {
            //     throw new SystemException("Unhandled ActorState");
            // }
        }
    }

    public void Draw(Monster monster, SpriteBatch spriteBatch)
    {
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
            new Vector2(0, 0),
            2f,
            effect,
            0f
        );
    }
}
