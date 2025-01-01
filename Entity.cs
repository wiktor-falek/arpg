using System;
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
  public float Speed { get; set; } = 25.0f;
  private float _angleDegrees = 360f;
  private float _currentDuration = 0f;
  private readonly float _maxDuration = 5.0f;
  private Texture2D _rectangleTexture;

  public void LoadAssets(ContentManager contentManager)
  {

  }

  public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
  {
    _rectangleTexture = new Texture2D(device, 1, 1);
    _rectangleTexture.SetData([Color.White]);

    if (_rectangleTexture != null)
    {
      var rectangle = new Rectangle((int)Position.X, (int)Position.Y, 20, 20);
      spriteBatch.Draw(_rectangleTexture, rectangle, Color.Red);
    }
  }

  public void Update(GameTime gameTime)
  {
    var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
    _currentDuration += elapsedTime;

    if (_currentDuration >= _maxDuration)
    {
      int index = arpg.Game1.Entities.FindIndex(e => e.Id == Id);
      arpg.Game1.Entities.RemoveAt(index);
      return;
    }

    double angleRadians = _angleDegrees * Math.PI / 180;
    double x = Position.X + (Speed * elapsedTime * Math.Cos(angleRadians));
    double y = Position.Y + (Speed * elapsedTime * Math.Sin(angleRadians));
    Position = new((float)x, (float)y);
  }
}