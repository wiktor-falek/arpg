using arpg;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class InventoryUI
{
    public bool IsOpen = false;
    public Rectangle WindowBounds;
    public Item? HoveredItem;

    private Player _player;
    const int SQUARE_SIZE = 24;
    const int BORDER_SIZE = 1;

    public InventoryUI(Player player)
    {
        _player = player;
        int windowWidth = Game1.NativeResolution.Width / 2 - 20;
        int screenWidth = Game1.NativeResolution.Width;
        int screenHeight = Game1.NativeResolution.Height;
        WindowBounds = new(screenWidth - windowWidth, 0, windowWidth, screenHeight);
    }

    public void Update(GameTime gameTime)
    {
        HoveredItem = _player.Inventory.GetItem(0, 0);
        Vector2 mousePosition = MouseManager.GetMousePosition();

        // TODO: find hovered item
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsOpen)
            return;
        DrawWindow(spriteBatch);
        DrawEquipment(spriteBatch);
        DrawInventory(spriteBatch);
        DrawTooltip(spriteBatch);
    }

    public bool OnClick(Vector2 mousePosition)
    {
        if (!IsOpen)
            return false;

        bool cursorWithin_windowBounds =
            mousePosition.X > WindowBounds.Left
            && mousePosition.X < WindowBounds.Right
            && mousePosition.Y > WindowBounds.Top
            && mousePosition.Y < WindowBounds.Bottom;

        if (!cursorWithin_windowBounds)
            return false;

        // handle click
        return true;
    }

    private void DrawWindow(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Assets.RectangleTexture,
            WindowBounds,
            null,
            Color.Black,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.UIWindow
        );
    }

    private void DrawEquipment(SpriteBatch spriteBatch)
    {
        int spacing = 3;
        int equipmentWidth = SQUARE_SIZE * 8 + spacing * 4;

        int currentX = WindowBounds.Left + ((WindowBounds.Width - equipmentWidth) / 2);
        int currentY = 10;

        // main hand
        DrawSquare(spriteBatch, new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 4));
        _player.Equipment.MainHand?.Draw(spriteBatch, currentX, currentY);

        currentY += SQUARE_SIZE * 4 + spacing;

        // gloves
        DrawSquare(spriteBatch, new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 2));
        _player.Equipment.Gloves?.Draw(spriteBatch, currentX, currentY);
        currentY += SQUARE_SIZE;
        currentX += SQUARE_SIZE * 2 + spacing;

        // left ring
        DrawSquare(spriteBatch, new(currentX, currentY, SQUARE_SIZE * 1, SQUARE_SIZE * 1));
        _player.Equipment.LeftRing?.Draw(spriteBatch, currentX, currentY);
        currentX += SQUARE_SIZE + spacing;

        // belt
        DrawSquare(spriteBatch, new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 1));
        _player.Equipment.Belt?.Draw(spriteBatch, currentX, currentY);
        currentY -= SQUARE_SIZE * 3 + spacing;

        // chest
        DrawSquare(spriteBatch, new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 3));
        _player.Equipment.Chest?.Draw(spriteBatch, currentX, currentY);
        currentY -= SQUARE_SIZE * 2 + spacing;

        // helmet
        DrawSquare(spriteBatch, new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 2));
        _player.Equipment.Head?.Draw(spriteBatch, currentX, currentY);
        currentY += SQUARE_SIZE * 5 + spacing * 2;
        currentX += SQUARE_SIZE * 2 + spacing;

        // right ring
        DrawSquare(spriteBatch, new(currentX, currentY, SQUARE_SIZE * 1, SQUARE_SIZE * 1));
        _player.Equipment.RightRing?.Draw(spriteBatch, currentX, currentY);
        currentY -= (int)(SQUARE_SIZE * 3.5) + spacing * 2;

        // amulet
        DrawSquare(spriteBatch, new(currentX, currentY, SQUARE_SIZE * 1, SQUARE_SIZE * 1));
        _player.Equipment.Amulet?.Draw(spriteBatch, currentX, currentY);
        currentY += (int)(SQUARE_SIZE * 3.5) + spacing * 2;
        currentX += SQUARE_SIZE + spacing;
        currentY -= SQUARE_SIZE;

        // boots
        DrawSquare(spriteBatch, new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 2));
        _player.Equipment.Boots?.Draw(spriteBatch, currentX, currentY);
        currentY -= SQUARE_SIZE * 4 + spacing;

        // offhand
        DrawSquare(spriteBatch, new(currentX, currentY, SQUARE_SIZE * 2, SQUARE_SIZE * 4));
        _player.Equipment.OffHand?.Draw(spriteBatch, currentX, currentY);
    }

    private void DrawInventory(SpriteBatch spriteBatch)
    {
        int inventoryHeight = _player.Inventory.Height * (SQUARE_SIZE + BORDER_SIZE) - 1;
        int inventoryWidth = _player.Inventory.Width * (SQUARE_SIZE + BORDER_SIZE) - 1;

        for (int i = 0; i < _player.Inventory.Width; i++)
        {
            for (int j = 0; j < _player.Inventory.Height; j++)
            {
                int x = WindowBounds.Left + i * (SQUARE_SIZE + BORDER_SIZE);
                int y = WindowBounds.Bottom - inventoryHeight + j * (SQUARE_SIZE + BORDER_SIZE);

                DrawSquare(spriteBatch, new(x, y, SQUARE_SIZE, SQUARE_SIZE));

                Item? item = _player.Inventory.GetItem(i, j);
                if (item is not null && _player.Inventory.Grid.SquareIsOriginSquare(i, j))
                {
                    item.Draw(spriteBatch, x, y);
                }
            }
        }
    }

    private void DrawSquare(SpriteBatch spriteBatch, Rectangle rectangle)
    {
        spriteBatch.Draw(
            Assets.RectangleTexture,
            rectangle,
            null,
            Color.DarkSlateGray,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            Layer.UIWindowElement
        );
    }

    private void DrawTooltip(SpriteBatch spriteBatch)
    {
        if (HoveredItem is null)
            return;

        HoveredItem.DrawTooltip(spriteBatch);
    }
}
