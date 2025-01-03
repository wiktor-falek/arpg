using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace arpg;

public class Game1 : Game
{
    public static Player Player;
    public static List<IActor> Actors = []; // Non-Player actors
    public static List<IEntity> Entities = [];
    public float Scale;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _renderTarget;

    private Hud _hud;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public static void RemoveEntity(IEntity entity)
    {
        int index = Entities.FindIndex(e => e.Id == entity.Id);
        Entities.RemoveAt(index);
    }

    public static void RemoveActor(IActor actor)
    {
        int index = Actors.FindIndex(e => e.Id == actor.Id);
        Actors.RemoveAt(index);
    }

    protected override void Initialize()
    {
        base.Initialize();
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 360;
        _graphics.ApplyChanges();

        Player = new Player { Position = new(100, 100) };
        for (int i = 0; i < 3; i++)
        {
            Monster monster = new() { Position = new(600, 200 + 100 * i) };
            Actors.Add(monster);
        }
        _hud = new Hud();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _renderTarget = new(GraphicsDevice, 1920, 1080);
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

        for (int i = Actors.Count - 1; i >= 0; i--)
        {
            IActor actor = Actors[i];
            actor.Update(gameTime);
        }

        for (int i = Entities.Count - 1; i >= 0; i--)
        {
            IEntity entity = Entities[i];
            entity.Update(gameTime);
        }

        _hud.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Scale = 1f / (1080f / _graphics.GraphicsDevice.Viewport.Height);

        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.AliceBlue);

        _spriteBatch.Begin();

        foreach (var actor in Actors)
        {
            actor.Draw(_spriteBatch, GraphicsDevice);
        }

        Player.Draw(_spriteBatch, GraphicsDevice);

        foreach (var entity in Entities)
        {
            entity.Draw(_spriteBatch, GraphicsDevice);
        }

        _hud.Draw(_spriteBatch, GraphicsDevice);

        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.AliceBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(
            _renderTarget,
            Vector2.Zero,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            Scale,
            SpriteEffects.None,
            0f
        );
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
