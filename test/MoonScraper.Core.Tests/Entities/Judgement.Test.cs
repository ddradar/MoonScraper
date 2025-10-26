using MoonScraper.Core.Entities;

namespace MoonScraper.Core.Tests.Entities;

/// <summary>
/// Unit test for <see cref="Judgement"/>.
/// </summary>
[TestClass]
public sealed class JudgementTest
{
    private static readonly Random _random = new();

    public static IEnumerable<TestDataRow<(int, int, int, int, int, int)>> ConstructorTestData()
        => Enumerable.Range(0, 5).Select<int, TestDataRow<(int, int, int, int, int, int)>>(_ =>
        {
            int marvelous = _random.Next(0, ushort.MaxValue);
            int perfect = _random.Next(0, ushort.MaxValue);
            int great = _random.Next(0, ushort.MaxValue);
            int good = _random.Next(0, ushort.MaxValue);
            int miss = _random.Next(0, ushort.MaxValue);
            int ok = _random.Next(0, ushort.MaxValue);
            return new((marvelous, perfect, great, good, miss, ok))
            {
                DisplayName = $"{nameof(Judgement)}({marvelous}, {perfect}, {great}, {good}, {miss}, {ok}) > Creates instance"
            };
        });

    /// <summary>
    /// <see cref="Judgement(in int, in int, in int, in int, in int, in int)"/> with valid values should create an instance correctly.
    /// </summary>
    /// <param name="marvelous"><inheritdoc cref="Judgement" path="/param[@name='Marvelous']"/></param>
    /// <param name="perfect"><inheritdoc cref="Judgement" path="/param[@name='Perfect']"/></param>
    /// <param name="great"><inheritdoc cref="Judgement" path="/param[@name='Great']"/></param>
    /// <param name="good"><inheritdoc cref="Judgement" path="/param[@name='Good']"/></param>
    /// <param name="miss"><inheritdoc cref="Judgement" path="/param[@name='Miss']"/></param>
    /// <param name="ok"><inheritdoc cref="Judgement" path="/param[@name='OK']"/></param>
    [TestMethod(DisplayName = $"{nameof(Judgement)} > Constructor (int) > Creates instance when arg is")]
    [DynamicData(nameof(ConstructorTestData))]
    [DataRow(0, 0, 0, 0, 0, 0, DisplayName = $"{nameof(Judgement)}(0, 0, 0, 0, 0, 0) > Creates instance")]
    [DataRow(65535, 0, 0, 0, 0, 0, DisplayName = $"{nameof(Judgement)}(65535, 0, 0, 0, 0, 0) > Creates instance")]
    [DataRow(65535, 65535, 65535, 65535, 65535, 65535, DisplayName = $"{nameof(Judgement)}(65535, 65535, 65535, 65535, 65535, 65535) > Creates instance")]
    public void Constructor_Int_With_ValidValues_Should_Create_Instance(int marvelous, int perfect, int great, int good, int miss, int ok)
        => new Judgement(marvelous, perfect, great, good, miss, ok)
            .ShouldSatisfyAllConditions(
                j => j.Marvelous.ShouldBe((ushort)marvelous),
                j => j.Perfect.ShouldBe((ushort)perfect),
                j => j.Great.ShouldBe((ushort)great),
                j => j.Good.ShouldBe((ushort)good),
                j => j.Miss.ShouldBe((ushort)miss),
                j => j.OK.ShouldBe((ushort)ok)
            );

    /// <summary>
    /// <see cref="Judgement(in int, in int, in int, in int, in int, in int)"/> with invalid values should throw <see cref="ArgumentOutOfRangeException"/> about <paramref name="argName"/>.
    /// </summary>
    /// <param name="marvelous"><inheritdoc cref="Judgement" path="/param[@name='Marvelous']"/></param>
    /// <param name="perfect"><inheritdoc cref="Judgement" path="/param[@name='Perfect']"/></param>
    /// <param name="great"><inheritdoc cref="Judgement" path="/param[@name='Great']"/></param>
    /// <param name="good"><inheritdoc cref="Judgement" path="/param[@name='Good']"/></param>
    /// <param name="miss"><inheritdoc cref="Judgement" path="/param[@name='Miss']"/></param>
    /// <param name="ok"><inheritdoc cref="Judgement" path="/param[@name='OK']"/></param>
    /// <param name="argName">The name of the argument that causes the exception.</param>
    [TestMethod]
    [DataRow(-1, 0, 0, 0, 0, 0, nameof(marvelous), DisplayName = $"{nameof(Judgement)}({nameof(marvelous)}: -1) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, -1, 0, 0, 0, 0, nameof(perfect), DisplayName = $"{nameof(Judgement)}({nameof(perfect)}: -1) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, -1, 0, 0, 0, nameof(great), DisplayName = $"{nameof(Judgement)}({nameof(great)}: -1) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, 0, -1, 0, 0, nameof(good), DisplayName = $"{nameof(Judgement)}({nameof(good)}: -1) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, 0, 0, -1, 0, nameof(miss), DisplayName = $"{nameof(Judgement)}({nameof(miss)}: -1) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, 0, 0, 0, -1, nameof(ok), DisplayName = $"{nameof(Judgement)}({nameof(ok)}: -1) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(65536, 0, 0, 0, 0, 0, nameof(marvelous), DisplayName = $"{nameof(Judgement)}({nameof(marvelous)}: 65536) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 65536, 0, 0, 0, 0, nameof(perfect), DisplayName = $"{nameof(Judgement)}({nameof(perfect)}: 65536) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, 65536, 0, 0, 0, nameof(great), DisplayName = $"{nameof(Judgement)}({nameof(great)}: 65536) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, 0, 65536, 0, 0, nameof(good), DisplayName = $"{nameof(Judgement)}({nameof(good)}: 65536) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, 0, 0, 65536, 0, nameof(miss), DisplayName = $"{nameof(Judgement)}({nameof(miss)}: 65536) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, 0, 0, 0, 65536, nameof(ok), DisplayName = $"{nameof(Judgement)}({nameof(ok)}: 65536) > Throws {nameof(ArgumentOutOfRangeException)}")]
    public void Constructor_Int_Should_Throw_ArgumentOutOfRangeException(int marvelous, int perfect, int great, int good, int miss, int ok, string argName)
        => Should.Throw<ArgumentOutOfRangeException>(() => new Judgement(marvelous, perfect, great, good, miss, ok))
            .ParamName.ShouldBe(argName);

    /// <summary>
    /// <see cref="Judgement.ExScore"/> should calculate EX SCORE correctly.
    /// </summary>
    /// <param name="marvelous"><inheritdoc cref="Judgement" path="/param[@name='Marvelous']"/></param>
    /// <param name="perfect"><inheritdoc cref="Judgement" path="/param[@name='Perfect']"/></param>
    /// <param name="great"><inheritdoc cref="Judgement" path="/param[@name='Great']"/></param>
    /// <param name="good"><inheritdoc cref="Judgement" path="/param[@name='Good']"/></param>
    /// <param name="miss"><inheritdoc cref="Judgement" path="/param[@name='Miss']"/></param>
    /// <param name="ok"><inheritdoc cref="Judgement" path="/param[@name='OK']"/></param>
    /// <param name="expected">Expected EX SCORE value.</param>
    [TestMethod]
    [DataRow(0, 0, 0, 0, 0, 0, 0, DisplayName = $"{nameof(Judgement)}(0, 0, 0, 0, 0, 0) > .{nameof(Judgement.ExScore)} > Returns 0")]
    [DataRow(100, 0, 0, 0, 0, 0, 300, DisplayName = $"{nameof(Judgement)}({nameof(marvelous)}: 100) > .{nameof(Judgement.ExScore)} > Returns 300")]
    [DataRow(0, 100, 0, 0, 0, 0, 200, DisplayName = $"{nameof(Judgement)}({nameof(perfect)}: 100) > .{nameof(Judgement.ExScore)} > Returns 200")]
    [DataRow(0, 0, 100, 0, 0, 0, 100, DisplayName = $"{nameof(Judgement)}({nameof(great)}: 100) > .{nameof(Judgement.ExScore)} > Returns 100")]
    [DataRow(0, 0, 0, 100, 0, 0, 0, DisplayName = $"{nameof(Judgement)}({nameof(good)}: 100) > .{nameof(Judgement.ExScore)} > Returns 0")]
    [DataRow(0, 0, 0, 0, 100, 0, 0, DisplayName = $"{nameof(Judgement)}({nameof(miss)}: 100) > .{nameof(Judgement.ExScore)} > Returns 0")]
    [DataRow(0, 0, 0, 0, 0, 100, 300, DisplayName = $"{nameof(Judgement)}({nameof(ok)}: 100) > .{nameof(Judgement.ExScore)} > Returns 300")]
    [DataRow(50, 30, 20, 10, 5, 40, 350, DisplayName = $"{nameof(Judgement)}(50, 30, 20, 10, 5, 40) > .{nameof(Judgement.ExScore)} > Returns 350")]
    [DataRow(100, 50, 25, 15, 10, 20, 485, DisplayName = $"{nameof(Judgement)}(100, 50, 25, 15, 10, 20) > .{nameof(Judgement.ExScore)} > Returns 485")]
    [DataRow(65535, 65535, 65535, 0, 0, 65535, 393207, DisplayName = $"{nameof(Judgement)}(65535, 65535, 65535, 0, 0, 65535) > .{nameof(Judgement.ExScore)} > Returns 393207")]
    public void ExScore_Should_Return_Expected(int marvelous, int perfect, int great, int good, int miss, int ok, int expected)
        => new Judgement(marvelous, perfect, great, good, miss, ok).ExScore
            .ShouldBe(expected);
}
