using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DroppedItem
{
    public Item Item;
    public Vector2 Position;
    private Vector2 _stringOrigin;
    private Rectangle _bounds;

    public DroppedItem(Item item, Vector2 position)
    {
        Item = item;
        Position = position;
        _stringOrigin = Assets.Fonts.MonogramExtened.MeasureString(Item.Name);
        _bounds = new Rectangle((int)Position.X, (int)Position.Y, (int)_stringOrigin.X + 16, 16);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 playerAimCoordinate = Camera.CameraOrigin + MouseManager.GetInGameMousePosition();

        // TODO: factor in border
        bool cursorWithinBounds =
            playerAimCoordinate.X > _bounds.Left
            && playerAimCoordinate.X < _bounds.Right
            && playerAimCoordinate.Y > _bounds.Top
            && playerAimCoordinate.Y < _bounds.Bottom;

        int borderThickness = 1;

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
            cursorWithinBounds ? Color.White : new Color(0, 0, 0, 0.6f),
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.DroppedItem
        );

        spriteBatch.DrawString(
            Assets.Fonts.MonogramExtened,
            Item.Name,
            new((int)Position.X + (_bounds.Width - _stringOrigin.X) / 2, (int)Position.Y),
            cursorWithinBounds ? Color.Black : Color.White,
            0f,
            new(0, 0),
            1f,
            SpriteEffects.None,
            Layer.DroppedItemText
        );
    }
}
