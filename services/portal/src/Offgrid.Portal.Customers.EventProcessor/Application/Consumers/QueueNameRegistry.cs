namespace Offgrid.Portal.Customers.EventProcessor.Application.Consumers;

public static class QueueNameRegistry
{
    public const string CustomerChangedEventQueue = "offgrid.portal.customers.customer-changed";
    public const string CustomerSuspendedEventQueue = "offgrid.portal.customers.customer-suspended";
    public const string CustomerReinstatedEventQueue = "offgrid.portal.customers.customer-reinstated";
}
