using System;

public class PlayerLevel {
    public int Current { get; private set; }
    public int CurrentXP { get; private set; }
    public int RequiredXP { get; private set; }

    private const int LEVEL_CAP = 100;
    private const float GROWTH_RATE = 1.5f;

    public PlayerLevel()
    {
        Current = 1;
        CurrentXP = 0;
        RequiredXP = GetRequiredXP(Current);
    }

    public void GrantXP(int amount)
    {
        if (Current >= LEVEL_CAP) return;

        CurrentXP += amount;
        while (CurrentXP > RequiredXP)
        {
            int leftoverXP = CurrentXP + amount - RequiredXP;
            CurrentXP = leftoverXP;
            RequiredXP = GetRequiredXP(++Current);
        }
    }

    private int GetRequiredXP(int level)
    {
        return (int)Math.Floor(100 * Current * GROWTH_RATE);
    }
}
