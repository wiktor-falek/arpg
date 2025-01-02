using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace arpg;

public class Game1 : Game
{
    public static List<IEntity> Entities = [];
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private Monster _monster;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
        _player = new Player();
        _monster = new Monster();
        _player.Position = new(0, 0);
        _monster.Position = new(200, 100);
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

        _player.Update(gameTime);
        _monster.Update(gameTime);

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
        _player.Draw(_spriteBatch);
        _monster.Draw(_spriteBatch);

        foreach (var entity in Entities)
        {
            entity.Draw(_spriteBatch, GraphicsDevice);
        }

        string playerHealthText = $"Player HP: {_player.Health}";
        string playerPositionText = $"Player X: {_player.Position.X} Y: {_player.Position.Y}";

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

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
