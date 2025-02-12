using arpg;

public class Sandals : EquippableItem
{
    public Sandals(): base("Sandals", Rarity.Normal, EquippableSlot.Boots, width: 2, height: 2, Assets.Items.None_1x1) // TODO: 2x2
    { 
        BaseAffixes.Add(new MovementSpeedAffix(5));
    }
}
