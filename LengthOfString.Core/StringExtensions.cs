
namespace LengthOfString.Core;

public static class StringExtensions
{
    public static bool ContainsWord(this string? s, string word) =>

        s != null && s.Split(new[] { ' ' }).Contains(word);

    public static int Length(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return 0;
        }

        return Services.StringUtilities.Length(input);
    }
}
