using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public enum Rarity
{
    Normal,
    Magic,
    Rare,
    Unique,
    Set,
}

public class Item
{
    public string Name { get; private set; }
    public Rarity Rarity { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Asset Asset;
    
    public Item(string name, Rarity rarity, int width, int height, Asset asset)
    {
        Name = name;
        Rarity = rarity;
        Width = width;
        Height = height;
        Asset = asset;
    }

    public void Draw(SpriteBatch spriteBatch, int x, int y)
    {
        spriteBatch.Draw(
            Asset.Texture,
            new(x, y, Width * 24, Height * 24),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.Item
        );
    }
}
