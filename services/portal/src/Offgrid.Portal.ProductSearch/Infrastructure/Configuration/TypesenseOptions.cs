using Typesense.Setup;

namespace Offgrid.Portal.ProductSearch.Infrastructure.Configuration;

public sealed class TypesenseOptions
{
    public const string SectionName = "TypesenseOptions";
    public string ApiKey { get; set; } = string.Empty;
    public List<Node> Nodes { get; set; } = [];
}
