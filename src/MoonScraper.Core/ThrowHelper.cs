using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MoonScraper.Core;

internal static class ThrowHelper
{
    internal static ushort ThrowIfNotUInt16(in int value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value, paramName);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, ushort.MaxValue, paramName);
        return (ushort)value;
    }

    [DoesNotReturn]
    internal static void ThrowArgumentException(string paramName, string? message = null)
        => throw new ArgumentException(message, paramName);
}
