using System;
using System.Collections.Generic;

public static class GlobalLootTable
{
    private static List<Type> _pool =
    [
        typeof(Hood),
        typeof(Sandals),
        typeof(RubyRing),
        typeof(SapphireRing),
        typeof(AugmentingCore),
    ];
    private static Random _rng = new();

    public static Item GenerateItem(int ilvl)
    {
        int index = _rng.Next(_pool.Count);
        Item item = (Item)Activator.CreateInstance(_pool[index]);

        if (item is EquippableItem equippable)
        {
            equippable.ItemLevel(ilvl);
        }

        return item;
    }
}
