namespace MoonScraper.Core.Entities;

public enum ClearLamp
{
    Undefined = -1,
    Failed = 0,
    /// <summary>Use CUT, FREEZE OFF, or JUMP OFF option</summary>
    AssistedClear = 1,
    /// <summary>Normal clear</summary>
    Clear = 2,
    /// <summary>Use LIFE4 gauge option (Failed on 4 misses)</summary>
    Life4 = 3,
    /// <summary>No misses</summary>
    FullCombo = 4,
    /// <summary>Full Combo with only Great or better judgments</summary>
    GreatFullCombo = 5,
    /// <summary>Full Combo with only Perfect or better judgments</summary>
    PerfectFullCombo = 6,
    /// <summary>Full Combo with only Marvelous judgments (Max score)</summary>
    MarvelousFullCombo = 7,
}
