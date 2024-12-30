using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class GraphicsComponent
{
    private Texture2D IdleTexture;
    private Texture2D WalkTexture;

    private List<Rectangle> IdleFrames = [];
    private List<Rectangle> WalkFrames = [];

    private int CurrentFrame = 0;
    private float FrameTime = 0.1f;
    private float ElapsedTime = 0f;

    public void Draw(Player player, SpriteBatch spriteBatch)
    {
        var texture = player.state == PlayerState.Idling ? IdleTexture : WalkTexture;
        var frame =
            player.state == PlayerState.Idling
                ? IdleFrames[CurrentFrame]
                : WalkFrames[CurrentFrame];
        var effect =
            player.facing == PlayerFacing.Right
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

    public void Update(Player player, GameTime gameTime)
    {
        ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (ElapsedTime >= FrameTime)
        {
            ElapsedTime = 0f;

            CurrentFrame++;

            if (player.state == PlayerState.Walking)
            {
                if (CurrentFrame >= WalkFrames.Count)
                {
                    CurrentFrame = 0;
                }
            }
            else if (player.state == PlayerState.Idling)
            {
                if (CurrentFrame >= IdleFrames.Count)
                {
                    CurrentFrame = 0;
                }
            }
            else
            {
                throw new SystemException("Unhandled PlayerState");
            }
        }
    }

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

    public void ResetFrames()
    {
        CurrentFrame = 0;
    }
}
