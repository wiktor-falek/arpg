using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class MonsterGraphicsComponent
{
    private List<Texture2D> IdleTextures = [];

    // private List<Texture2D> WalkTextures = [];

    private int CurrentFrame = 0;
    private float FrameTime = 0.25f;
    private float ElapsedTime = 0f;

    public void LoadAssets(ContentManager content)
    {
        IdleTextures.Add(content.Load<Texture2D>("skeleton_ready_1"));
        IdleTextures.Add(content.Load<Texture2D>("skeleton_ready_2"));
        IdleTextures.Add(content.Load<Texture2D>("skeleton_ready_3"));
    }

    public void Update(Monster monster, GameTime gameTime)
    {
        ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (ElapsedTime >= FrameTime)
        {
            ElapsedTime = 0f;

            CurrentFrame++;

            if (monster.State == ActorState.Idling)
            {
                if (CurrentFrame >= IdleTextures.Count)
                {
                    CurrentFrame = 0;
                }
            }
            // else if (monster.State == ActorState.Idling)
            // {
            //     if (CurrentFrame >= IdleFrames.Count)
            //     {
            //         CurrentFrame = 0;
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
        var texture = IdleTextures[CurrentFrame];
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
