using System.Text.Json;
using Offgrid.Framework.Messaging;
using Offgrid.Framework.System.Text;
using Offgrid.Portal.Customers.Contracts.DomainEvents;
using Spectre.Console;
using Spectre.Console.Json;

namespace Offgrid.Portal.Customers.EventProcessor.Application.EventHandlers;

public sealed class ConsoleCustomerChangedEventHandler : IEventHandler<CustomerChangedEvent>
{
    public Task<bool> HandleAsync(CustomerChangedEvent @event, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(@event, nameof(@event));

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule($"[white]Customer Changed Event Received[/] [dim]{DateTime.Now:HH:mm:ss}[/]") { Style = Style.Parse("blue") });

        var table = new Table().Border(TableBorder.Rounded).BorderColor(Color.Blue);

        table.AddColumn(new TableColumn("Property").LeftAligned());
        table.AddColumn(new TableColumn("Value").LeftAligned());

        table.AddRow("Aggregate ID", @event.AggregateId ?? "[dim]—[/]");
        table.AddRow("Type", $"[bold]{@event.EventType}[/]");
        table.AddRow("Type ID", $"[bold]{@event.EventTypeId}[/]");
        table.AddRow("Correlation ID", @event.CorrelationId ?? "[dim]—[/]");
        table.AddRow("Time", @event.OccurredAt.ToString("o") ?? "[dim]—[/]");
        table.AddRow("Customer ID", @event.CustomerId.ToString());
        table.AddRow("Changed By", @event.ChangedBy ?? "[dim]—[/]");
        table.AddRow("Changes:");

        if (@event.Changes != null)
        {
            foreach (var change in @event.Changes)
            {
                table.AddRow($"  - {change.PropertyName}", $"[dim]{change.OldValue}[/] → [green]{change.NewValue}[/]");
            }
        }

        AnsiConsole.Write(table);

        var json = JsonSerializer.Serialize(@event, JsonSerializationOptions.Pretty);
        var jsonText = new JsonText(json)
            .MemberColor(Color.Cyan)
            .StringColor(Color.Green)
            .NumberColor(Color.Blue)
            .BooleanColor(Color.Yellow);

        var panel = new Panel(jsonText)
        {
            Header = new PanelHeader("Payload"),
            Border = BoxBorder.Rounded,
            BorderStyle = Style.Parse("blue")
        };

        AnsiConsole.Write(panel);

        return Task.FromResult(true);
    }
}
