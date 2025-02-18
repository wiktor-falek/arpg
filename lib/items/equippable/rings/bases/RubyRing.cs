using arpg;

public class RubyRing : EquippableItem
{
    public RubyRing()
        : base(
            name: "Ruby Ring",
            rarity: Rarity.Normal,
            slot: EquippableSlot.Ring,
            level: 1,
            levelRequirement: 1,
            width: 1,
            height: 1,
            asset: Assets.Items.None_1x1
        )
    {
        BaseAffixes.Add(new LifeAffix(8, 10));
    }
}
