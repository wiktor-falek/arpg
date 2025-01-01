using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public interface IEntity
{
  string Id { get; set; }
  void LoadAssets(ContentManager contentManager);
  void Update(GameTime gameTime);
  void Draw(SpriteBatch spriteBatch, GraphicsDevice device);
}

public class Projectile : IEntity
{
  public string Id { get; set; } = "uuid or something"; // TODO: generate id
  public Vector2 Position { get; set; }
  public float Speed { get; set; } = 5.0f;
  private float currentDuration = 0f;
  private readonly float maxDuration = 5.0f;
  private Texture2D rectangleTexture;

  public void LoadAssets(ContentManager contentManager)
  {

  }

  public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
  {
    rectangleTexture = new Texture2D(device, 1, 1);
    rectangleTexture.SetData([Color.White]);

    if (rectangleTexture != null)
    {
      var rectangle = new Rectangle((int)Position.X, (int)Position.Y, 20, 20);
      spriteBatch.Draw(rectangleTexture, rectangle, Color.Red);
    }
  }

  public void Update(GameTime gameTime)
  {
    var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
    currentDuration += elapsedTime;

    if (currentDuration >= maxDuration)
    {
      int index = arpg.Game1.Entities.FindIndex(e => e.Id == Id);
      arpg.Game1.Entities.RemoveAt(index);
      return;
    }

    // @TODO: delta time
    Vector2 temp = Position;
    temp.X += 1.1f * Speed;
    temp.Y += 1.1f * Speed;
    Position = temp;
  }
}