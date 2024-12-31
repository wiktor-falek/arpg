using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class PlayerGraphicsComponent
{
    private Texture2D IdleTexture;
    private Texture2D WalkTexture;

    private List<Rectangle> IdleFrames = [];
    private List<Rectangle> WalkFrames = [];

    private int CurrentFrame = 0;
    private float FrameTime = 0.1f;
    private float ElapsedTime = 0f;

    public void LoadAssets(ContentManager content)
    {
        IdleTexture = content.Load<Texture2D>("PlayerIdle");
        WalkTexture = content.Load<Texture2D>("PlayerWalk");

        // TOOD: cut further
        // 1400x140
        // int frameWidth = 33;
        // int frameHeight = 53;

        for (int i = 0; i < 10; i++)
        {
            IdleFrames.Add(new Rectangle(140 * i, 0, 140, 140));
        }

        for (int i = 0; i < 8; i++)
        {
            WalkFrames.Add(new Rectangle(140 * i, 0, 140, 140));
        }
    }

    public void Update(Player player, GameTime gameTime)
    {
        ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (ElapsedTime >= FrameTime)
        {
            ElapsedTime = 0f;

            CurrentFrame++;

            if (player.State == ActorState.Idling)
            {
                if (CurrentFrame >= IdleFrames.Count)
                {
                    CurrentFrame = 0;
                }
            }
            else if (player.State == ActorState.Walking)
            {
                if (CurrentFrame >= WalkFrames.Count)
                {
                    CurrentFrame = 0;
                }
            }
            else
            {
                throw new SystemException("Unhandled ActorState");
            }
        }
    }

    public void Draw(Player player, SpriteBatch spriteBatch)
    {
        var texture = player.State == ActorState.Idling ? IdleTexture : WalkTexture;
        var frame =
            player.State == ActorState.Idling ? IdleFrames[CurrentFrame] : WalkFrames[CurrentFrame];
        var effect =
            player.Facing == ActorFacing.Right
                ? SpriteEffects.None
                : SpriteEffects.FlipHorizontally;

        spriteBatch.Draw(
            texture,
            player.Position,
            frame,
            Color.White,
            0f,
            new Vector2(0, 0),
            2f,
            effect,
            0f
        );
    }

    public void ResetFrames()
    {
        CurrentFrame = 0;
    }
}
