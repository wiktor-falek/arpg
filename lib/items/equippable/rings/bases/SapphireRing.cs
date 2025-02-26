using arpg;

public class SapphireRing : EquippableItem
{
    public SapphireRing()
        : base(
            name: "Sapphire Ring",
            rarity: Rarity.Normal,
            slot: EquippableSlot.Ring,
            level: 1,
            levelRequirement: 1,
            width: 1,
            height: 1,
            asset: Assets.Items.None_1x1
        )
    {
        BaseAffixes.Add(new ManaAffix(8, 10));
    }
}
