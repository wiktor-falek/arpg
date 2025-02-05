using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace arpg;

public class Game1 : Game
{
    public bool IsRunning = true;
    public static Config Config { get; private set; }

    public struct NativeResolution
    {
        public const int Width = 640;
        public const int Height = 360;
    }

    public static new GraphicsDevice GraphicsDevice;
    public static KeyboardInputManager KeyboardInputManager;
    public static InputMapper InputMapper;
    private GraphicsDeviceManager _graphics;
    private RenderTarget2D _renderTarget;
    private SpriteBatch _spriteBatch;
    private Background _background;
    private PauseMenu _pauseMenu;
    private Hud _hud;
    private UI _ui;
    private GameInputController _gameInputController;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        KeyboardInputManager = new KeyboardInputManager(this);
        InputMapper = new InputMapper(KeyboardInputManager);
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
        Config = new Config(_graphics, GraphicsDevice, _renderTarget);
        _background = new Background();
        _hud = new Hud();
        _ui = new UI();
        _pauseMenu = new PauseMenu();
        _gameInputController = new GameInputController(_ui, _pauseMenu);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Assets.Load(Content, GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardInputManager.Update();

        if (GameState.IsRunning)
        {
            GameState.Update(gameTime);
            _hud.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);
        _background.Draw(_spriteBatch);
        _spriteBatch.End();

        _spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            transformMatrix: Camera.Transform,
            samplerState: SamplerState.PointClamp
        );

        foreach (var actor in GameState.Actors)
        {
            actor.Draw(_spriteBatch);
        }

        foreach (var entity in GameState.Entities)
        {
            entity.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        _spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

        _hud.Draw(_spriteBatch, GraphicsDevice);

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
