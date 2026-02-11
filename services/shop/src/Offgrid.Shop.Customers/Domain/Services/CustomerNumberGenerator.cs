using NanoidDotNet;

namespace Offgrid.Shop.Customers.Domain.Services;

public sealed class CustomerNumberGenerator : ICustomerNumberGenerator
{
    private const string SafeAlphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
    private const int RandomPartLength = 8;
    private const string Prefix = "CUST-";

    public string GenerateCustomerNumber()
    {
        var randomPart = Nanoid.Generate(
            alphabet: SafeAlphabet,
            size: RandomPartLength);

        return Prefix + randomPart;
    }
}
