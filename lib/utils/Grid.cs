using System;

class GridItem<T>(T value, int originX, int originY)
{
    public T Value = value;
    public bool IsOriginSquare = false;
    public int OriginX = originX;
    public int OriginY = originY;

    public override string ToString()
    {
        return $"{Value?.ToString() ?? "null"}";
    }
}

#nullable enable
public class Grid<T> where T : class
{
    public int Width;
    public int Height;

    private GridItem<T?>[,] Squares;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        Squares = new GridItem<T?>[Height, Width];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Squares[y, x] = new(null, default, default);
            }
        }
    }

    public bool AddItem(T item, int originX, int originY, int width, int height)
    {
        if (!ItemFits(originX, originY, width, height))
            return false;

        for (int x = originX; x < originX + width; x++)
        {
            for (int y = originY; y < originY + height; y++)
            {
                // update existing instead of new instance?
                Squares[y, x] = new GridItem<T?>(item, originX, originY);
            }
        }

        Squares[originY, originX].IsOriginSquare = true;

        return true;
    }

    public T? GetItem(int x, int y)
    {
        GridItem<T?> gridItem = Squares[y, x];
        return gridItem.Value;
    }

    public bool SquareIsOriginSquare(int x, int y)
    {
        return Squares[y, x].IsOriginSquare;
    }

    public bool SquareExists(int x, int y)
    {
        return (x < 0 || x > Width || y < 0 || y > Height);
    }

    public bool SquareIsTaken(int x, int y)
    {
        return Squares[y, x].Value != null;
    }

    public bool ItemFits(int x, int y, int width, int height)
    {
        // for (int i = x; i < width + 1; i++)
        // {
        //     if (!SquareExists(x, y) || SquareIsTaken(x, y))
        //         return false;
        // }

        // for (int i = y; i < height + 1; i++)
        // {
        //     if (!SquareExists(x, y) || SquareIsTaken(x, y))
        //         return false;
        // }

        return true;
    }
}
#nullable disable
