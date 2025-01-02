using System;
using System.Collections.Generic;
using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Fireball : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public float Speed { get; set; } = 350f;
    public double Angle = 0d;
    private float _currentDuration = 0f;
    private readonly float _maxDuration = 2f;
    private List<Texture2D> _textures = Assets.GetTextures(
        "fireball_1",
        "fireball_2",
        "fireball_3",
        "fireball_4",
        "fireball_5"
    );
    private int _currentFrame = 0;

    public void LoadAssets(ContentManager contentManager) { }

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
    {
        Texture2D texture = _textures[_currentFrame];
        _currentFrame++;
        if (_currentFrame >= _textures.Count)
            _currentFrame = 0;

        spriteBatch.Draw(
            texture,
            Position,
            null,
            Color.White,
            (float)Angle,
            new Vector2(texture.Width / 2, texture.Height / 2),
            1f,
            SpriteEffects.None,
            0f
        );
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
