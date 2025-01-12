using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HolyFire(Player player) : IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vector2 Position { get; set; }
    public IHitbox Hitbox
    {
        get => new CircleHitbox(Position, Radius);
        set => _hitbox = value;
    }
    public double Radius = 100d;

    private Player _player = player;
    private IHitbox _hitbox;

    private float _tickRate = 0.25f;
    private float _frameTime = 0f;

    public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)
    {
        Texture2D circleTexture = CreateCircleTexture(device, (int)Radius);

        spriteBatch.Draw(
            circleTexture,
            new((int)(Position.X - Radius), (int)(Position.Y - Radius)),
            null,
            new Color(205, 45, 10, 64),
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.PlayerOnGroundEffect
        );
    }

    public void Update(GameTime gameTime)
    {
        _frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        Position = new(_player.Position.X + 0, _player.Position.Y + 20);
        if (_frameTime >= _tickRate)
        {
            foreach (var actor in GameState.Actors)
            {
                if (actor is Monster && actor.Hitbox.Intersects(Hitbox))
                {
                    actor.TakeDamage(5);
                }
            }
            _frameTime -= _tickRate;
        }
    }

    Texture2D CreateCircleTexture(GraphicsDevice device, int radius)
    {
        int diameter = radius * 2;
        Texture2D texture = new Texture2D(device, diameter, diameter);
        Color[] colorData = new Color[diameter * diameter];

        float radiusSquared = radius * radius;

        for (int x = 0; x < diameter; x++)
        {
            for (int y = 0; y < diameter; y++)
            {
                int index = x + y * diameter;
                Vector2 pos = new(x - radius, y - radius);
                if (pos.LengthSquared() <= radiusSquared)
                {
                    colorData[index] = Color.White;
                }
                else
                {
                    colorData[index] = Color.Transparent;
                }
            }
        }

        texture.SetData(colorData);
        return texture;
    }
}
