public class Item(string name)
{
    public string Name = name;
    public int Width = 2;
    public int Height = 3;

    public override string ToString()
    {
        return Name;
    }
}

public class Inventory(Player player)
{
    public readonly int Width = 12;
    public readonly int Height = 6;

    private Player _player = player;
    private Grid<Item> _grid;

    public void AddItem(Item item, int x, int y)
    {
        _grid = new Grid<Item>(Width, Height);
        _grid.AddItem(item, x, y, item.Width, item.Height);
    }
}
