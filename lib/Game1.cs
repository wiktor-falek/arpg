﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace arpg;

public class Game1 : Game
{
    public static Config Config { get; private set; }

    public struct NativeResolution
    {
        public const int Width = 640;
        public const int Height = 360;
    }

    public static World World;
    public static new GraphicsDevice GraphicsDevice;
    public static InputManager InputManager;

    private GraphicsDeviceManager _graphics;
    private RenderTarget2D _renderTarget;
    private SpriteBatch _spriteBatch;
    private Background _background;
    private LootUI _lootUI;
    private PauseMenu _pauseMenu;
    private Hud _hud;
    private GameUI _gameUI;
    private GameInputController _gameInputController;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        InputManager = new InputManager();
        Content.RootDirectory = "Content";
        Window.Title = "Path of Exile 4";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        GraphicsDevice = base.GraphicsDevice;
        _renderTarget = new RenderTarget2D(
            GraphicsDevice,
            NativeResolution.Width,
            NativeResolution.Height
        );
        Assets.Load(Content, GraphicsDevice);
        Config = new Config(_graphics, GraphicsDevice, _renderTarget);

        Player player = new Player();
        World = new World(player);
        _background = new Background();
        _lootUI = new LootUI();
        _hud = new Hud();
        _gameUI = new GameUI(World.Player);
        _pauseMenu = new PauseMenu();

        _gameInputController = new GameInputController();
        _gameInputController.RegisterOnClose(_gameUI.OnClose);
        _gameInputController.RegisterOnClose(_pauseMenu.OnClose);

        _gameInputController.RegisterOnLeftClick(_pauseMenu.OnLeftClick);
        _gameInputController.RegisterOnLeftClick(_gameUI.OnLeftClick);
        _gameInputController.RegisterOnLeftClick(World.Player.InputComponent.OnLeftClick);
        _gameInputController.RegisterOnLeftClickRelease(
            World.Player.InputComponent.OnLeftClickRelease
        );

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        InputManager.Update();

        if (GameState.IsRunning)
        {
            World.Update(gameTime);
            _hud.Update(gameTime);
        }

        Camera.Follow(World.Player);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // TODO: non-scaled render target

        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );
        _background.Draw(_spriteBatch);
        _spriteBatch.End();

        // world space rendering
        _spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            transformMatrix: Camera.Transform,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        foreach (IActor actor in World.Actors)
        {
            actor.Draw(_spriteBatch);
        }

        foreach (IEntity entity in World.Entities)
        {
            entity.Draw(_spriteBatch);
        }

        _lootUI.Draw(_spriteBatch);

        _spriteBatch.End();

        // screen space rendering
        _spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            samplerState: SamplerState.PointClamp,
            blendState: BlendState.AlphaBlend
        );

        _hud.Draw(_spriteBatch);
        _gameUI.Draw(_spriteBatch);
        _pauseMenu.Draw(_spriteBatch);

        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.White);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(_renderTarget, GraphicsDevice.Viewport.Bounds, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
