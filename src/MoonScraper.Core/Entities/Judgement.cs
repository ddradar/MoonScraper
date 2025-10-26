using System.Runtime.CompilerServices;

namespace MoonScraper.Core.Entities;

/// <summary>Judgement counts</summary>
public readonly record struct Judgement(ushort Marvelous = 0, ushort Perfect = 0, ushort Great = 0, ushort Good = 0, ushort Miss = 0, ushort OK = 0)
{
    public Judgement(in int marvelous, in int perfect, in int great, in int good, in int miss, in int ok)
        : this(
            ThrowHelper.ThrowIfNotUInt16(marvelous),
            ThrowHelper.ThrowIfNotUInt16(perfect),
            ThrowHelper.ThrowIfNotUInt16(great),
            ThrowHelper.ThrowIfNotUInt16(good),
            ThrowHelper.ThrowIfNotUInt16(miss),
            ThrowHelper.ThrowIfNotUInt16(ok)
        )
    { }

    /// <summary>EX SCORE</summary>
    public readonly int ExScore => CalcExScoreInner((ushort)(Marvelous + OK), Perfect, Great);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int CalcExScoreInner(ushort marvelousAndOk, ushort perfect, ushort great)
        => (marvelousAndOk * 3) + (perfect * 2) + great;
}
