using System;
using System.Collections.Generic;

public class LootPoolEntry(Func<Item> createItem, int weight)
{
    public Func<Item> CreateItem = createItem;
    public int Weight = weight;
}

public class LootSystem
{
    private readonly List<LootPoolEntry> _pool = [];
    private int _totalWeight = 0;
    private Random _random = new();

    public LootSystem()
    {
        AddEntry(() => new AugmentingCore(), 3000);
        AddEntry(() => new Hood(), 1000);
        AddEntry(() => new Sandals(), 1000);
        AddEntry(() => new RubyRing(), 200);
        AddEntry(() => new SapphireRing(), 200);
        AddEntry(() => new TheOneRubyRing(), 10);
    }

    public List<Item> GenerateLoot(IMonster monster, Player player)
    {
        List<Item> drops = [];

        int dropCount = RollDropCount();
        for (int i = 0; i < dropCount; i++)
        {
            int roll = _random.Next(0, _totalWeight);
            int cumulative = 0;
            foreach (var entry in _pool)
            {
                cumulative += entry.Weight;
                if (roll < cumulative)
                {
                    drops.Add(entry.CreateItem());
                    break;
                }
            }
        }

        return drops;
    }

    public void AddEntry(Func<Item> createItem, int weight)
    {
        _pool.Add(new LootPoolEntry(createItem, weight));
        _totalWeight += weight;
    }

    private int RollDropCount()
    {
        var weights = new Dictionary<int, int>
        {
            { 0, 9000 },
            { 1, 1000 },
            { 2, 60 },
            { 3, 5 },
        };

        int total = 0;
        foreach (var w in weights.Values)
            total += w;

        int roll = _random.Next(0, total);
        int cumulative = 0;
        foreach (var weight in weights)
        {
            cumulative += weight.Value;
            if (roll < cumulative)
                return weight.Key;
        }

        return 0;
    }

    // TODO: roll rarity
}
