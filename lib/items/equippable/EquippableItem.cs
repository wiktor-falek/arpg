using System.Collections.Generic;
using arpg;

public class EquippableItem : Item, IEquippable
{
    public List<Affix> BaseAffixes { get; private set; }
    public EquippableSlot Slot { get; private set; }

    public EquippableItem(
        string name,
        Rarity rarity,
        EquippableSlot slot,
        int width,
        int height,
        Asset asset
    )
        : base(name, rarity, width, height, asset)
    {
        BaseAffixes = [];
        Slot = slot;
    }

    public void Equip(Player player)
    {
        foreach (Affix affix in BaseAffixes)
        {
            affix.Apply(player, affix.RolledValue);
        }
    }

    public void Unequip(Player player)
    {
        foreach (Affix affix in BaseAffixes)
        {
            affix.Apply(player, -(affix.RolledValue));
        }
    }
}
