using System.Security.Cryptography;
using System.Text;

namespace MongoInit.Data;

public static class ProductIdGenerator
{
    public static string GenerateProductId(long productNumber, string brand)
    {
        // 1. Normalize and combine inputs
        var input = $"{brand.Trim().ToLowerInvariant()}:{productNumber}";
        var inputBytes = Encoding.UTF8.GetBytes(input);

        // 2. Compute Hash
        var hashBytes = SHA256.HashData(inputBytes);

        // 3. Convert to Hex string (Convert.ToHexString is fast and built-in)
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }
}
