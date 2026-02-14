using System.Text.Json;
using Offgrid.Framework.Messaging;
using Offgrid.Framework.System.Text;
using Offgrid.Portal.Customers.Contracts.DomainEvents;
using Spectre.Console;
using Spectre.Console.Json;

namespace Offgrid.Portal.Customers.EventProcessor.Application.EventHandlers;

public sealed class ConsoleCustomerReinstatedEventHandler : IEventHandler<CustomerReinstatedEvent>
{
    public Task<bool> HandleAsync(CustomerReinstatedEvent @event, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(@event, nameof(@event));

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule($"[white]Customer ReinstatedEvent Received[/] [dim]{DateTime.Now:HH:mm:ss}[/]") { Style = Style.Parse("green") });

        var table = new Table().Border(TableBorder.Rounded).BorderColor(Color.Green);

        table.AddColumn(new TableColumn("Property").LeftAligned());
        table.AddColumn(new TableColumn("Value").LeftAligned());

        table.AddRow("Aggregate ID", @event.AggregateId ?? "[dim]—[/]");
        table.AddRow("Type", $"[bold]{@event.EventType}[/]");
        table.AddRow("Type ID", $"[bold]{@event.EventTypeId}[/]");
        table.AddRow("Correlation ID", @event.CorrelationId ?? "[dim]—[/]");
        table.AddRow("Time", @event.OccurredAt.ToString("o") ?? "[dim]—[/]");
        table.AddRow("Customer ID", @event.CustomerId.ToString());
        table.AddRow("Reason", @event.Reason ?? "[dim]—[/]");

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
            BorderStyle = Style.Parse("green")
        };

        AnsiConsole.Write(panel);

        return Task.FromResult(true);
    }
}
