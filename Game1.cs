using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace arpg;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;
    private Player _player;
    private Monster _monster;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _player = new Player();
        _monster = new Monster();
    }

    protected override void Initialize()
    {
        _player.Position = new(100, 100);
        _monster.Position = new(200, 100);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("monogram_extended");
        _player.LoadAssets(Content);
        _monster.LoadAssets(Content);
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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.AliceBlue);

        _spriteBatch.Begin();
        _player.Draw(_spriteBatch);
        _monster.Draw(_spriteBatch);

        // Finds the center of the string in coordinates inside the text rectangle
        Vector2 textMiddlePoint = _font.MeasureString("Hello World") / 2;
        // Places text in center of the screen
        Vector2 position = new Vector2(
            Window.ClientBounds.Width / 2,
            Window.ClientBounds.Height / 2
        );
        _spriteBatch.DrawString(
            _font,
            "MonoGame Font Test",
            position,
            Color.White,
            0,
            textMiddlePoint,
            1.0f,
            SpriteEffects.None,
            0.5f
        );
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
