namespace Blazewind.Core.Helpers;

public static class StringExtensions
{
    public static string JoinClasses(this string? baseClass, params string?[] classes) => string.Join(' ',
        new[] { baseClass }
            .Concat(classes)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x!.Trim()));
}
