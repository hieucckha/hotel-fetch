namespace SomeSandwich.Hotel.Cli.Extensions;

/// <summary>
/// Provides extension methods for numeric operations.
/// </summary>
public static class NumericExtensions
{
    /// <summary>
    /// Determines whether the specified nullable double value is null or zero.
    /// </summary>
    /// <param name="value">The nullable double value to check.</param>
    /// <returns><c>true</c> if the value is null or zero; otherwise, <c>false</c>.</returns>
    public static bool IsNullOrZero(this double? value)
    {
        return value is null or 0;
    }
}
