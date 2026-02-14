using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Offgrid.Framework.RabbitMq;

public abstract class RabbitMqClientBase : IAsyncDisposable
{
    private readonly ILogger<RabbitMqClientBase> _logger;
    private readonly IConnectionFactory _connectionFactory;
    private readonly RabbitMqClientOptions _options;
    private IConnection? _connection;
    private IChannel? _channel;

    private readonly SemaphoreSlim _semaphore = new(1, 1);

    protected RabbitMqClientBase(
        ILogger<RabbitMqClientBase> logger,
        IConnectionFactory connectionFactory,
        IOptions<RabbitMqClientOptions> options)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(connectionFactory, nameof(connectionFactory));
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        _logger = logger;
        _connectionFactory = connectionFactory;
        _options = options.Value;
    }

    protected IConnection? Connection => _connection;
    protected IChannel? Channel => _channel;
    protected ILogger Logger => _logger;
    protected RabbitMqClientOptions Options => _options;

    protected async Task EnsureConnectedAsync(CancellationToken cancellationToken)
    {
        await EnsureConnectedAsync(null, cancellationToken);
    }

    protected async Task EnsureConnectedWithQueueAsync(QueueSettings queueSettings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(queueSettings, nameof(queueSettings));
        await EnsureConnectedAsync(queueSettings, cancellationToken);
    }

    private async Task EnsureConnectedAsync(QueueSettings? queueSettings, CancellationToken cancellationToken)
    {
        if (_connection?.IsOpen == true && _channel?.IsOpen == true)
            return;

        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            if (_connection?.IsOpen == true && _channel?.IsOpen == true)
                return;

            if (_connection?.IsOpen != true)
            {
                _connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);
            }

            if (_channel?.IsOpen != true)
            {
                _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

                await _channel.ExchangeDeclareAsync(
                    exchange: _options.ExchangeName,
                    type: _options.ExchangeType,
                    durable: true,
                    autoDelete: false,
                    cancellationToken: cancellationToken);

                if (queueSettings != null)
                {
                    await _channel.QueueDeclareAsync(
                        queue: queueSettings.Name,
                        durable: queueSettings.Durable,
                        exclusive: queueSettings.Exclusive,
                        autoDelete: queueSettings.AutoDelete,
                        cancellationToken: cancellationToken);

                    await _channel.QueueBindAsync(
                        queue: queueSettings.Name,
                        exchange: _options.ExchangeName,
                        routingKey: queueSettings.RoutingKey,
                        cancellationToken: cancellationToken);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to RabbitMQ");
            throw;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public virtual async ValueTask DisposeAsync()
    {
        if (_channel != null)
        {
            await _channel.DisposeAsync();
            _channel = null;
        }

        if (_connection != null)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }

        _semaphore.Dispose();
    }
}
