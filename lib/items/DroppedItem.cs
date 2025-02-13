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
        _bounds = new Rectangle((int)Position.X, (int)Position.Y, (int)_stringOrigin.X + 16, 16);
    }

    public void Update(GameTime gameTime)
    {
        Vector2 playerAimCoordinate = Camera.CameraOrigin + MouseManager.GetInGameMousePosition();
        // TODO: factor in border
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

        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle((int)Position.X, (int)Position.Y, _bounds.Width, borderThickness),
            Color.White
        );

        // Bottom border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle(
                (int)Position.X,
                (int)Position.Y + _bounds.Height - borderThickness,
                _bounds.Width,
                borderThickness
            ),
            Color.White
        );

        // Left border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle((int)Position.X, (int)Position.Y, borderThickness, _bounds.Height),
            Color.White
        );

        // Right border
        spriteBatch.Draw(
            Assets.RectangleTexture,
            new Rectangle(
                (int)Position.X + _bounds.Width - borderThickness,
                (int)Position.Y,
                borderThickness,
                _bounds.Height
            ),
            Color.White
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
            new((int)Position.X + (_bounds.Width - _stringOrigin.X) / 2, (int)Position.Y),
            IsHovered ? Color.Black : Color.White,
            0f,
            new(0, 0),
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
