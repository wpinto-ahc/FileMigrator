#nullable enable

namespace MigrateInvalidMemberLetters.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhitespace(this string text) => 
            string.IsNullOrWhiteSpace(text) || string.IsNullOrEmpty(text);
    }
}
