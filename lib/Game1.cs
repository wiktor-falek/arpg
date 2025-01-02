﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace arpg;

public class Game1 : Game
{
    public static Player Player;
    public static List<IEntity> Entities = [];
    public static List<IActor> Actors = []; // Non-Player actors
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Hud _hud;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _hud = new Hud();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
        Player = new Player { Position = new(100, 100) };

        Monster monster = new() { Position = new(600, 400) };
        Actors.Add(monster);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Assets.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        Player.Update(gameTime);

        foreach (var actor in Actors)
        {
            actor.Update(gameTime);
        }

        for (int i = Entities.Count - 1; i >= 0; i--)
        {
            var entity = Entities[i];
            entity.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.AliceBlue);

        _spriteBatch.Begin();
        Player.Draw(_spriteBatch, GraphicsDevice);

        foreach (var actor in Actors)
        {
            actor.Draw(_spriteBatch, GraphicsDevice);
        }

        foreach (var entity in Entities)
        {
            entity.Draw(_spriteBatch, GraphicsDevice);
        }

        string playerHealthText = $"Player HP: {Player.Health}";
        string playerPositionText = $"Player X: {Player.Position.X} Y: {Player.Position.Y}";

        float textScale = 1.0f;
        float layerdepth = 1.0f;
        float rotation = 0.0f;
        int fontHeight = 16;

        _spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            playerHealthText,
            new Vector2(0, fontHeight * 0),
            Color.Black,
            rotation,
            Vector2.Zero,
            textScale,
            SpriteEffects.None,
            layerdepth
        );

        _spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            playerPositionText,
            new Vector2(0, fontHeight * 1),
            Color.Black,
            rotation,
            Vector2.Zero,
            textScale,
            SpriteEffects.None,
            layerdepth
        );

        _hud.Draw(_spriteBatch, GraphicsDevice);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
