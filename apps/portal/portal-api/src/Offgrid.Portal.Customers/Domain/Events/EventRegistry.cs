namespace Offgrid.Portal.Customers.Domain.Events;

public static class EventRegistry
{
    public static class Customer
    {
        public const string CustomerChangedEventId = "1000";
        public const string CustomerSuspendedEventId = "1001";
        public const string CustomerReinstatedEventId = "1002";
    }
}
