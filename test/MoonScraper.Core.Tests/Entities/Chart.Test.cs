using System.Threading.Tasks;
using MoonScraper.Core.Entities;

namespace MoonScraper.Core.Tests.Entities;

/// <summary>
/// Unit test for <see cref="Chart"/>.
/// </summary>
[TestClass]
public sealed class ChartTest
{
    public TestContext TestContext { get; set; }

    private static readonly Random _random = new();
    public static IEnumerable<TestDataRow<(int, int, int)>> ConstructorTestData()
        => Enumerable.Range(0, 5).Select<int, TestDataRow<(int, int, int)>>(_ =>
        {
            int notes = _random.Next(0, ushort.MaxValue);
            int freezes = _random.Next(0, ushort.MaxValue);
            int shocks = _random.Next(0, ushort.MaxValue);
            return new((notes, freezes, shocks))
            {
                DisplayName = $"{nameof(Chart)}({notes}, {freezes}, {shocks}) > Creates instance"
            };
        });

    /// <summary>
    /// <see cref="Chart(in int, in int, in int)"/> with valid values should create an instance correctly.
    /// </summary>
    /// <param name="notes"><inheritdoc cref="Chart" path="/param[@name='Notes']"/></param>
    /// <param name="freezes"><inheritdoc cref="Chart" path="/param[@name='Freezes']"/></param>
    /// <param name="shocks"><inheritdoc cref="Chart" path="/param[@name='Shocks']"/></param>
    [TestMethod(DisplayName = $"{nameof(Chart)} > Constructor > Creates instance when arg is")]
    [DynamicData(nameof(ConstructorTestData))]
    [DataRow(0, 0, 0, DisplayName = $"{nameof(Chart)}({nameof(notes)}: 0, {nameof(freezes)}: 0, {nameof(shocks)}: 0) > Creates instance")]
    [DataRow(65535, 0, 0, DisplayName = $"{nameof(Chart)}({nameof(notes)}: 65535, {nameof(freezes)}: 0, {nameof(shocks)}: 0) > Creates instance")]
    [DataRow(65535, 65535, 65535, DisplayName = $"{nameof(Chart)}({nameof(notes)}: 65535, {nameof(freezes)}: 65535, {nameof(shocks)}: 65535) > Creates instance")]
    public void Constructor_With_ValidValues_Should_Create_Instance(int notes, int freezes, int shocks)
        => new Chart(notes, freezes, shocks)
            .ShouldSatisfyAllConditions(
                c => c.Notes.ShouldBe((ushort)notes),
                c => c.Freezes.ShouldBe((ushort)freezes),
                c => c.Shocks.ShouldBe((ushort)shocks)
            );

    /// <summary>
    /// <see cref="Chart(in int, in int, in int)"/> with invalid values should throw <see cref="ArgumentOutOfRangeException"/> about <paramref name="argName"/>.
    /// </summary>
    /// <param name="notes"><inheritdoc cref="Chart" path="/param[@name='Notes']"/></param>
    /// <param name="freezes"><inheritdoc cref="Chart" path="/param[@name='Freezes']"/></param>
    /// <param name="shocks"><inheritdoc cref="Chart" path="/param[@name='Shocks']"/></param>
    /// <param name="argName">The name of the argument that causes the exception.</param>
    [TestMethod]
    [DataRow(-1, 0, 0, nameof(notes), DisplayName = $"{nameof(Chart)}({nameof(notes)}: -1, {nameof(freezes)}: 0, {nameof(shocks)}: 0) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, -1, 0, nameof(freezes), DisplayName = $"{nameof(Chart)}({nameof(notes)}: 0, {nameof(freezes)}: -1, {nameof(shocks)}: 0) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, -1, nameof(shocks), DisplayName = $"{nameof(Chart)}({nameof(notes)}: 0, {nameof(freezes)}: 0, {nameof(shocks)}: -1) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(65536, 0, 0, nameof(notes), DisplayName = $"{nameof(Chart)}({nameof(notes)}: 65536, {nameof(freezes)}: 0, {nameof(shocks)}: 0) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 65536, 0, nameof(freezes), DisplayName = $"{nameof(Chart)}({nameof(notes)}: 0, {nameof(freezes)}: 65536, {nameof(shocks)}: 0) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, 65536, nameof(shocks), DisplayName = $"{nameof(Chart)}({nameof(notes)}: 0, {nameof(freezes)}: 0, {nameof(shocks)}: 65536) > Throws {nameof(ArgumentOutOfRangeException)}")]
    public void Constructor_Should_Throw_ArgumentOutOfRangeException(int notes, int freezes, int shocks, string argName)
        => Should.Throw<ArgumentOutOfRangeException>(() => new Chart(notes, freezes, shocks))
            .ParamName.ShouldBe(argName);

    /// <summary>
    /// Chart instance for test their methods.
    /// </summary>
    private static readonly Chart _chart = new(200, 30, 10);

    [TestMethod]
    [DataRow(201, 0, 0, 0, 0, 0, DisplayName = $"{nameof(Chart)}(200, 30, 10) > .{nameof(Chart.CalcNormalScore)}({nameof(marvelous)}: 201) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(100, 101, 0, 0, 0, 0, DisplayName = $"{nameof(Chart)}(200, 30, 10) > .{nameof(Chart.CalcNormalScore)}({nameof(marvelous)}: 100, {nameof(perfect)}: 101) > Throws {nameof(ArgumentOutOfRangeException)}")]
    [DataRow(0, 0, 0, 0, 0, 41, DisplayName = $"{nameof(Chart)}(200, 30, 10) > .{nameof(Chart.CalcNormalScore)}({nameof(ok)}: 41) > Throws {nameof(ArgumentOutOfRangeException)}")]
    public void CalcNormalScore_Should_Throw_ArgumentOutOfRangeException_When_Invalid_Judgement(int marvelous, int perfect, int great, int good, int miss, int ok)
        => Should.Throw<ArgumentOutOfRangeException>(() => new Chart(200, 30, 10).CalcNormalScore(new(marvelous, perfect, great, good, miss, ok)))
            .ParamName.ShouldBe("judgement");

    [TestMethod]
    [DataRow(200, 0, 0, 0, 0, 40, 1000000, DisplayName = $"{nameof(Chart)}(200, 30, 10) > .{nameof(Chart.CalcNormalScore)}({nameof(marvelous)}: 200, {nameof(ok)}: 40) > Returns 1000000")]
    [DataRow(160, 40, 0, 0, 0, 40, 999600, DisplayName = $"{nameof(Chart)}(200, 30, 10) > .{nameof(Chart.CalcNormalScore)}({nameof(marvelous)}: 160, {nameof(perfect)}: 40, {nameof(ok)}: 40) > Returns 999600")]
    [DataRow(0, 0, 200, 0, 0, 40, 664660, DisplayName = $"{nameof(Chart)}(200, 30, 10) > .{nameof(Chart.CalcNormalScore)}({nameof(great)}: 240) > Returns 664660")]
    [DataRow(0, 0, 0, 200, 0, 40, 331330, DisplayName = $"{nameof(Chart)}(200, 30, 10) > .{nameof(Chart.CalcNormalScore)}({nameof(good)}: 240) > Returns 331330")]
    [DataRow(0, 0, 0, 0, 20, 0, 0, DisplayName = $"{nameof(Chart)}(200, 30, 10) > .{nameof(Chart.CalcNormalScore)}({nameof(miss)}: 20) > Returns 0")]
    [DataRow(140, 20, 20, 20, 1, 39, 895230, DisplayName = $"{nameof(Chart)}(200, 30, 10) > .{nameof(Chart.CalcNormalScore)}(mixed) > Returns 895230")]
    public void CalcNormalScore_Should_Return_Expected(int marvelous, int perfect, int great, int good, int miss, int ok, int expected)
        => new Chart(200, 30, 10).CalcNormalScore(new(marvelous, perfect, great, good, miss, ok))
            .ShouldBe(expected);

    [TestMethod]
    [DataRow(1000000, ClearLamp.Undefined, (int[])[414], DisplayName = $"{nameof(Chart)}(138) > .{nameof(Chart.DetectExScore)}(1000000) > Returns [414]")]
    [DataRow(0, ClearLamp.MarvelousFullCombo, (int[])[414], DisplayName = $"{nameof(Chart)}(138) > .{nameof(Chart.DetectExScore)}(<any value>, ${nameof(ClearLamp.MarvelousFullCombo)}) > Returns [414]")]
    [DataRow(0, ClearLamp.Undefined, (int[])[0], DisplayName = $"{nameof(Chart)}(138) > .{nameof(Chart.DetectExScore)}(0) > Returns [0]")]
    [DataRow(999000, ClearLamp.PerfectFullCombo, (int[])[314], DisplayName = $"{nameof(Chart)}(138) > .{nameof(Chart.DetectExScore)}(999000, ${nameof(ClearLamp.PerfectFullCombo)}) > Returns [314]")]
    [DataRow(950360, ClearLamp.GreatFullCombo, (int[])[361], DisplayName = $"{nameof(Chart)}(138) > .{nameof(Chart.DetectExScore)}(950360, ${nameof(ClearLamp.GreatFullCombo)}) > Returns [361]")]
    [DataRow(969780, ClearLamp.FullCombo, (int[])[281], DisplayName = $"{nameof(Chart)}(138) > .{nameof(Chart.DetectExScore)}(969780, ${nameof(ClearLamp.FullCombo)}) > Returns [281]")]
    [DataRow(998000, ClearLamp.Undefined, (int[])[], DisplayName = $"{nameof(Chart)}(138) > .{nameof(Chart.DetectExScore)}(998000) > Returns []")] // Invalid Score
    public async Task DetectExScore_Should_Return_Expected(int score, ClearLamp clearLamp, int[] expected)
        => new Chart(138).DetectExScore(score, clearLamp, TestContext.CancellationToken)
            .ToBlockingEnumerable(TestContext.CancellationToken)
            .ShouldBe(expected, ignoreOrder: false);

    [TestMethod(DisplayName = $"{nameof(Chart)}(300, 30, 10) > .{nameof(Chart.DetectExScore)}(984500) > Returns [987, 986, 985, 928, 927, 926, 870, 869, 868, 811, 810, 754, 753, 752, 695, 694]")]
    public async Task DetectExScore_Should_Return_Expected()
        => new Chart(300, 30, 10).DetectExScore(984500, cancellationToken: TestContext.CancellationToken)
            .ToBlockingEnumerable(TestContext.CancellationToken)
            .ShouldBe([987, 986, 985, 928, 927, 926, 870, 869, 868, 811, 810, 754, 753, 752, 695, 694], ignoreOrder: false);

    [TestMethod(DisplayName = $"{nameof(Chart)}(1000) > .{nameof(Chart.DetectExScore)}(500000, token) > Should throw {nameof(OperationCanceledException)} when cancelled")]
    public async Task DetectExScore_Should_Throw_OperationCanceledException_When_Cancelled()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        var chart = new Chart(1000); // Large chart to ensure long execution

        // Act & Assert
        var exception = await Should.ThrowAsync<OperationCanceledException>(async () =>
        {
            await foreach (int exScore in chart.DetectExScore(500000, cancellationToken: cts.Token))
                ; // Should not reach here due to immediate cancellation
        });
        exception.CancellationToken.ShouldBe(cts.Token);
    }
}
