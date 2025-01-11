using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace arpg;

public class Game1 : Game
{
    public static Config Config { get; private set; }
    private GraphicsDeviceManager _graphics;
    private RenderTarget2D _renderTarget;
    private SpriteBatch _spriteBatch;
    private KeyboardInputManager _keyboardInputManager;
    private Hud _hud;
    private Background _background;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _renderTarget = new(GraphicsDevice, 640, 360);
        _keyboardInputManager = new KeyboardInputManager(this);
        Content.RootDirectory = "Content";
        Window.Title = "Path of Exile 4";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Config = new(_graphics, GraphicsDevice, _renderTarget);
        _hud = new Hud();
        _background = new Background();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Assets.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        _keyboardInputManager.Update();

        GameState.Player.Update(gameTime);
        Camera.Follow(GameState.Player);

        for (int i = GameState.Actors.Count - 1; i >= 0; i--)
        {
            IActor actor = GameState.Actors[i];
            actor.Update(gameTime);
        }

        for (int i = GameState.Entities.Count - 1; i >= 0; i--)
        {
            IEntity entity = GameState.Entities[i];
            entity.Update(gameTime);
        }

        _hud.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(_renderTarget);
        GraphicsDevice.Clear(Color.AliceBlue);

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
            actor.Draw(_spriteBatch, GraphicsDevice);
        }

        GameState.Player.Draw(_spriteBatch, GraphicsDevice);

        foreach (var entity in GameState.Entities)
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
