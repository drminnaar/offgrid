
namespace Offgrid.Framework.RabbitMq;

public sealed record RabbitMqClientOptions
{
    public const string SectionName = "RabbitMqClient";

    private const string DefaultHostName = "localhost";
    private const int DefaultPort = 5672;
    private const string DefaultUserName = "guest";
    private const string DefaultPassword = "guest";
    private const string DefaultVirtualHost = "/";

    public string HostName { get; init; } = DefaultHostName;
    public int Port { get; init; } = DefaultPort;
    public string UserName { get; init; } = DefaultUserName;
    public string Password { get; init; } = DefaultPassword;
    public string VirtualHost { get; init; } = DefaultVirtualHost;
    public string ExchangeName { get; init; } = string.Empty;
    public string ExchangeType { get; init; } = string.Empty;
}
