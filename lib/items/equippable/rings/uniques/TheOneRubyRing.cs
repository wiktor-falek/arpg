using System.Collections.Generic;

public class TheOneRubyRing : RubyRing, IUnique
{
    public string UniqueName => "The One";
    public string UniqueFlavorText =>
        "One Ring to rule them all,\n"
        + "One Ring to find them,\n"
        + "One Ring to bring them all\n"
        + "and in the darkness bind them.";

    public List<Affix> UniqueAffixes { get; } =
        [
            new StrengthAffix(3, 5).RollValue(),
            new AgilityAffix(3, 5).RollValue(),
            new IntelligenceAffix(3, 5).RollValue(),
            new VitalityAffix(3, 5).RollValue(),
            new SpiritAffix(3, 5).RollValue(),
        ];

    public TheOneRubyRing()
        : base()
    {
        Rarity = Rarity.Unique;
    }
}
