using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace consumer_complaints.Services;

public static partial class ReferenceNumberGenerator
{
    private const string Alphabet = "23456789ABCDEFGHJKMNPQRSTUVWXYZ";
    private const int RandomLength = 6;

    [GeneratedRegex(@"^C-\d{2}-[23456789ABCDEFGHJKMNPQRSTUVWXYZ]{6}$")]
    private static partial Regex FormatRegex();

    public static string Generate(DateTime? utcNow = null)
    {
        var utc = utcNow ?? DateTime.UtcNow;
        var bytes = RandomNumberGenerator.GetBytes(RandomLength);
        var chars = new char[RandomLength];

        for (var i = 0; i < RandomLength; i++)
        {
            chars[i] = Alphabet[bytes[i] % Alphabet.Length];
        }

        return $"C-{utc:yy}-{new string(chars)}";
    }

    public static bool IsWellFormed(string? value)
        => !string.IsNullOrWhiteSpace(value) && FormatRegex().IsMatch(value);
}