using Typesense.Setup;

namespace Offgrid.Shop.Products.Infrastructure.Config;

public sealed record TypesenseOptions
{
    public const string SectionName = "TypesenseOptions";
    public string ApiKey { get; set; } = string.Empty;
    public List<Node> Nodes { get; set; } = [];
}
