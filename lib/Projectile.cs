using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Projectile : IEntity
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public Vector2 Position { get; set; }
  public float Speed { get; set; } = 350f;
  public double Angle = 0d;
  private float _currentDuration = 0f;
  private readonly float _maxDuration = 2f;
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

    double x = Position.X + (Speed * elapsedTime * Math.Cos(Angle));
    double y = Position.Y + (Speed * elapsedTime * Math.Sin(Angle));
    Position = new((float)x, (float)y);
  }
}