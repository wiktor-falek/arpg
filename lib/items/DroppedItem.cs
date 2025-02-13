using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DroppedItem
{
    public Item Item;
    public Vector2 Position;
    public bool IsHovered = false;
    private Vector2 _stringOrigin;
    private Rectangle _bounds;

    public DroppedItem(Item item, Vector2 position)
    {
        Item = item;
        Position = position;
        _stringOrigin = Assets.Fonts.MonogramExtened.MeasureString(Item.Name);

        const int HEIGHT = 16;
        int WIDTH = (int)_stringOrigin.X + 16;
        _bounds = new Rectangle(
            (int)Position.X - WIDTH / 2,
            (int)Position.Y - HEIGHT / 2,
            WIDTH,
            HEIGHT
        );
    }

    public void Update(GameTime gameTime)
    {
        Vector2 playerAimCoordinate = Camera.CameraOrigin + MouseManager.GetInGameMousePosition();
        bool cursorWithinBounds =
            playerAimCoordinate.X > _bounds.Left
            && playerAimCoordinate.X < _bounds.Right
            && playerAimCoordinate.Y > _bounds.Top
            && playerAimCoordinate.Y < _bounds.Bottom;
        IsHovered = cursorWithinBounds;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        const int borderThickness = 1;

        spriteBatch.Draw(Assets.RectangleTexture, _bounds, Color.Transparent);

        // Top border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle((int)_bounds.X, (int)_bounds.Y, _bounds.Width, borderThickness),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItemBorder
        );

        // Bottom border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle(
                (int)_bounds.X,
                (int)_bounds.Y + _bounds.Height - borderThickness,
                _bounds.Width,
                borderThickness
            ),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItemBorder
        );

        // Left border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle((int)_bounds.X, (int)_bounds.Y, borderThickness, _bounds.Height),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItemBorder
        );

        // Right border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle(
                (int)_bounds.X + _bounds.Width - borderThickness,
                (int)_bounds.Y,
                borderThickness,
                _bounds.Height
            ),
            null,
            Color.White,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItemBorder
        );

        spriteBatch.Draw(
            Assets.RectangleTexture,
            _bounds,
            null,
            IsHovered ? Color.White : new Color(0, 0, 0, 0.6f),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItem
        );

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            Item.Name,
            new((int)_bounds.X + (_bounds.Width - _stringOrigin.X) / 2, (int)_bounds.Y),
            IsHovered ? Color.Black : Color.White,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            Layer.DroppedItemText
        );
    }

    public bool GetPickedUp(Player player)
    {
        bool added = player.Inventory.AddItem(this.Item);
        return added;
    }
}
