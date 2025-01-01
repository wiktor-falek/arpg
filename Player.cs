using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Player : IActor
{
    public ActorState State { get; set; } = ActorState.Idling;
    public ActorFacing Facing { get; set; } = ActorFacing.Right;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public float Speed { get; set; } = 250f;
    public uint Health { get; set; } = 500u;

    private PlayerInputComponent _inputComponent = new();
    private PlayerGraphicsComponent _graphicsComponent = new();

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

    public void TransitionState(ActorState newState)
    {
        bool stateChanged = State != newState;
        if (stateChanged)
        {
            State = newState;
            _graphicsComponent.ResetFrames();
        }
    }

    public void Attack() { }
}
