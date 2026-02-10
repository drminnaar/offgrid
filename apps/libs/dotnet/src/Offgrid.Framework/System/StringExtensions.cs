using System.Text.Json;

namespace Offgrid.Framework.System;

public static class StringExtensions
{
    extension(string source)
    {
        public bool IsJson()
        {
            if (string.IsNullOrWhiteSpace(source)) return false;

            try
            {
                using var _ = JsonDocument.Parse(source);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        public string ToCamelCase()
        {
            if (string.IsNullOrEmpty(source) || char.IsLower(source[0]))
                return source;

            return char.ToLowerInvariant(source[0]) + source.Substring(1);
        }

        public string ToTitleCase()
        {
            var trimmedSource = source.Trim().ToLower();

            if (string.IsNullOrEmpty(trimmedSource))
            {
                return trimmedSource;
            }

            // Split by spaces and capitalize each word
            var words = trimmedSource.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Select(word =>
                char.ToUpper(word[0]) + word.Substring(1).ToLower()));
        }
    }
}
