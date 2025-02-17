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
    public bool IsHovered { get; set; } = false;
    public string Name { get; private set; }
    public Rarity Rarity { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Asset Asset;

    private const int ITEM_1x1_SIZE = 20;

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
            new(x, y, Width * ITEM_1x1_SIZE, Height * ITEM_1x1_SIZE),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.Item
        );
    }

    public void DrawTooltip(SpriteBatch spriteBatch)
    {
        int tooltipWidth = 150;
        int tooltipHeight = 100;

        Rectangle tooltipRect = new(
            (Game1.NativeResolution.Width - tooltipWidth) / 2,
            (Game1.NativeResolution.Height - tooltipHeight) / 2,
            tooltipWidth,
            tooltipHeight
        );

        // background
        spriteBatch.Draw(
            Assets.RectangleTexture,
            tooltipRect,
            null,
            new Color(0, 0, 0, 0.8f),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.ItemTooltipBackground
        );

        // item name
        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            Name,
            new Vector2(tooltipRect.X + 5, tooltipRect.Y + 5),
            Color.White,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.ItemTooltipText
        );
    }
}
