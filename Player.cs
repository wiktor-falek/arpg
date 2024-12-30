using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public enum PlayerState
{
    Idling,
    Walking,
}

public enum PlayerFacing
{
    Left,
    Right,
}

public class Player
{
    private InputComponent _inputComponent = new();
    private GraphicsComponent _graphicsComponent = new();

    public PlayerState state = PlayerState.Idling;
    public PlayerFacing facing = PlayerFacing.Right;

    public Vector2 Position;
    public float Speed = 250f;

    public void LoadAssets(ContentManager contentManager)
    {
        _graphicsComponent.LoadAssets(contentManager);
    }

    public void Update(GameTime gameTime)
    {
        _inputComponent.Update(this, gameTime);
        _graphicsComponent.Update(this, gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _graphicsComponent.Draw(this, spriteBatch);
    }

    public void TransitionState(PlayerState newState)
    {
        bool stateChanged = state != newState;
        if (stateChanged)
        {
            state = newState;
            _graphicsComponent.ResetFrames();
        }
    }
}
