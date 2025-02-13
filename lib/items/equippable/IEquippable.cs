using System.Collections.Generic;

public enum EquippableSlot
{
    MainHand,
    OffHand,
    Chest,
    Head,
    Gloves,
    Boots,
    Belt,
    Amulet,
    Ring,
}

public interface IEquippable
{
    EquippableSlot Slot { get; }
    void Equip(Player player);
    void Unequip(Player player);
}
