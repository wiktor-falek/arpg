using arpg;

public class Hood : EquippableItem
{
    public Hood()
        : base(
            "Hood",
            Rarity.Normal,
            EquippableSlot.Head,
            width: 2,
            height: 2,
            Assets.Items.None_2x2
        )
    {
        BaseAffixes.Add(new EvasionAffix(6, 10));
    }
}
