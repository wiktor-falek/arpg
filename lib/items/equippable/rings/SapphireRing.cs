using arpg;

public class SapphireRing : EquippableItem
{
    public SapphireRing()
        : base(
            "Sapphire Ring",
            Rarity.Normal,
            EquippableSlot.Ring,
            width: 1,
            height: 1,
            Assets.Items.None_1x1
        )
    {
        BaseAffixes.Add(new LifeAffix(8, 10));
    }
}
