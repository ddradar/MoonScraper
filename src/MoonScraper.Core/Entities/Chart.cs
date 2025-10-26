using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MoonScraper.Core.Entities;

/// <summary>Song's chart info</summary>
/// <param name="Notes">Note counts</param>
/// <param name="Freezes">Freeze Arrow counts</param>
/// <param name="Shocks">Shock Arrow counts</param>
public readonly record struct Chart(ushort Notes, ushort Freezes, ushort Shocks = 0)
{
    /// <summary>Creates an instance of Chart.</summary>
    /// <param name="notes"><inheritdoc cref="Chart" path="/param[@name='Notes']"/></param>
    /// <param name="freezes"><inheritdoc cref="Chart" path="/param[@name='Freezes']"/></param>
    /// <param name="shocks"><inheritdoc cref="Chart" path="/param[@name='Shocks']"/></param>
    /// <exception cref="ArgumentOutOfRangeException">Any parameter is negative or exceeds <see cref="ushort.MaxValue"/>.</exception>
    public Chart(in int notes, in int freezes = 0, in int shocks = 0)
        : this(ThrowHelper.ThrowIfNotUInt16(notes), ThrowHelper.ThrowIfNotUInt16(freezes), ThrowHelper.ThrowIfNotUInt16(shocks)) { }

    /// <summary>Maximum EX SCORE</summary>
    public readonly int MaxExScore => (Notes + Freezes + Shocks) * 3;

    /// <summary>
    /// Calculate normal score (0-1000000) from <paramref name="judgement"/>.
    /// </summary>
    /// <param name="judgement">Judgement counts</param>
    public readonly int CalcNormalScore(in Judgement judgement)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(judgement.Marvelous + judgement.Perfect + judgement.Great + judgement.Good, Notes, nameof(judgement));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(judgement.OK, Freezes + Shocks, nameof(judgement));

        return CalcNormalScoreInner((ushort)(judgement.Marvelous + judgement.OK), judgement.Perfect, judgement.Great, judgement.Good);
    }

    private readonly int CalcNormalScoreInner(ushort marvelousAndOk, ushort perfect, ushort great, ushort good)
    {
        decimal totalNotes = Notes + Freezes + Shocks;
        int res = (int)Math.Floor(
            ((
                (100000 * (marvelousAndOk + perfect))
                + (60000 * great)
                + (20000 * good)
            ) / totalNotes)
            - perfect
            - great
            - good
        ) * 10;
        Debug.Assert(res is >= 0 and <= 1000000);
        return res;
    }

    /// <summary>
    /// Enumerate possible EX SCORE from given <paramref name="score"/> and <paramref name="clearLamp"/>.
    /// </summary>
    /// <param name="score">Normal score</param>
    /// <param name="clearLamp">Clear lamp</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public readonly async IAsyncEnumerable<int> DetectExScore(int score, ClearLamp clearLamp = ClearLamp.Undefined, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(score);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(score, 1000000);
        if (score % 10 != 0)
            ThrowHelper.ThrowArgumentException(nameof(score), "Score must be a multiple of 10.");

        ushort totalNotes = (ushort)(Notes + Freezes + Shocks);

        // In MFC or PFC, EX is fixed. (MaxExScore - Perfect count)
        if (score == 1000000 || clearLamp == ClearLamp.MarvelousFullCombo)
        {
            yield return MaxExScore;
            yield break;
        }
        if (clearLamp == ClearLamp.PerfectFullCombo)
        {
            yield return MaxExScore - ((1000000 - score) / 10);
            yield break;
        }

        if (score == 0)
        {
            yield return 0;
            yield break;
        }

        var res = new SortedSet<int>();

        ushort maxMiss = clearLamp >= ClearLamp.FullCombo ? (ushort)0 :
                        clearLamp >= ClearLamp.Life4 ? Math.Min((ushort)3, totalNotes) :
                        totalNotes;
        for (ushort miss = 0; miss <= maxMiss; miss++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            ushort noMissNotes = (ushort)(totalNotes - miss);

            // Early pruning: check if maximum possible score is achievable
            if (score > CalcNormalScoreInner(noMissNotes, 0, 0, 0))
                break;

            ushort maxGood = clearLamp >= ClearLamp.GreatFullCombo ? (ushort)0 : noMissNotes;
            for (ushort good = 0; good <= maxGood; good++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                ushort maxGreat = (ushort)(noMissNotes - good);
                Debug.Assert(maxGreat <= noMissNotes);

                // Early exit if minimum score with this good count is too high
                if (score < CalcNormalScoreInner(0, 0, maxGreat, good))
                    continue;
                // Early exit if maximum score with this good count is too low
                if (score > CalcNormalScoreInner(maxGreat, 0, 0, good))
                    break;

                for (ushort great = 0; great <= maxGreat; great++)
                {
                    ushort maxPerfect = (ushort)(noMissNotes - good - great);
                    Debug.Assert(maxPerfect <= noMissNotes);

                    // Early exit if minimum score with this perfect count is too high
                    if (score < CalcNormalScoreInner(0, maxPerfect, great, good))
                        continue;
                    // Early exit if maximum score with this perfect count is too low
                    if (score > CalcNormalScoreInner(maxPerfect, 0, great, good))
                        break;

                    for (ushort perfect = 0; perfect <= maxPerfect; perfect++)
                    {
                        ushort marvelous = (ushort)(noMissNotes - good - great - perfect);
                        Debug.Assert(marvelous <= noMissNotes);

                        int calcedScore = CalcNormalScoreInner(marvelous, perfect, great, good);
                        if (score == calcedScore)
                        {
                            int exScore = Judgement.CalcExScoreInner(marvelous, perfect, great);
                            Debug.Assert(exScore >= 0 && exScore <= MaxExScore);
                            res.Add(exScore);
                        }
                        else if (score > calcedScore)
                        {
                            break;
                        }
                    }
                }
            }
        }
        foreach (int ex in res.Reverse())
            yield return ex;
    }
}
