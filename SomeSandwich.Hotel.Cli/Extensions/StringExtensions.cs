using System.Text.RegularExpressions;

namespace SomeSandwich.Hotel.Cli.Extensions;

/// <summary>
/// Provides extension methods for <see cref="string"/>.
/// </summary>
public static partial class StringExtensions
{
    [GeneratedRegex(@"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])")]
    private static partial Regex SplitPascalCaseRegex();

    /// <summary>
    /// Trims the string and converts it to lowercase.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <returns>A trimmed and lowercase version of the string.</returns>
    public static string BeautifyAndToLower(this string value)
    {
        return value.Beautify().ToLower();
    }

    /// <summary>
    /// Trims the string, splits PascalCase words, and converts them to lowercase.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <returns>A string with PascalCase words split and converted to lowercase.</returns>
    public static string BeatifyAndSplitAndToLower(this string value)
    {
        var words = SplitPascalCaseRegex()
            .Split(value.Beautify())
            .Select(v => v.ToLower());

        return string.Join(" ", words);
    }

    /// <summary>
    /// Trims the specified string, removing all leading and trailing white-space characters.
    /// </summary>
    /// <param name="value">The string to beautify.</param>
    /// <returns>A trimmed version of the string.</returns>
    public static string Beautify(this string value)
    {
        return value.Trim();
    }

    /// <summary>
    /// Trims the specified string, removing all leading and trailing white-space characters.
    /// </summary>
    /// <param name="value">The string to beautify.</param>
    /// <returns>A trimmed version of the string, or null if the input string is null.</returns>
    public static string? BeautifyNullable(this string? value)
    {
        return value?.Trim();
    }
}
