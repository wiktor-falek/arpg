using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace arpg;

public static class Layer
{
    public const float Text = 0.1f;
    public const float Entity = 0.2f;
    public const float Player = 0.3f;
    public const float Monster = 0.4f;
    public const float Hitbox = 0.5f;
}

public class Game1 : Game
{
    public static Player Player;
    public static List<IActor> Actors = []; // Non-Player actors
    public static List<IEntity> Entities = [];
    public static float ScaleX;
    public static float ScaleY;
    public static Camera Camera;
    public Config Config;
    private GraphicsDeviceManager _graphics;
    private RenderTarget2D _renderTarget;
    private SpriteBatch _spriteBatch;
    private Hud _hud;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Camera = new();
        Config = new(_graphics);
        Config.ChangeResolutionScale(3);
        Config.SetFullScreen();
        Config.ApplyChanges();
        Content.RootDirectory = "Content";
        Window.Title = "Path of Exile 4";
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
        _renderTarget = new(GraphicsDevice, 640, 360);
        ScaleX = (float)GraphicsDevice.Viewport.Width / _renderTarget.Width;
        ScaleY = (float)GraphicsDevice.Viewport.Height / _renderTarget.Height;
        base.Initialize();
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
        Camera.Follow(Player);

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
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.AliceBlue);

        _spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            transformMatrix: Camera.Transform,
            samplerState: SamplerState.PointClamp
        );

        foreach (var actor in Actors)
        {
            actor.Draw(_spriteBatch, GraphicsDevice);
        }

        Player.Draw(_spriteBatch, GraphicsDevice);

        foreach (var entity in Entities)
        {
            entity.Draw(_spriteBatch, GraphicsDevice);
        }

        _spriteBatch.End();

        _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

        _hud.Draw(_spriteBatch, GraphicsDevice);
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(_renderTarget, GraphicsDevice.Viewport.Bounds, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
