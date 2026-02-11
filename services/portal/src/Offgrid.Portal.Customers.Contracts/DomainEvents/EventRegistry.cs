namespace Offgrid.Portal.Customers.Contracts.DomainEvents;

public static class EventRegistry
{
    public static class Customer
    {
        public const string CustomerChangedEventTypeId = "CUST1000";
        public const string CustomerSuspendedEventTypeId = "CUST1001";
        public const string CustomerReinstatedEventTypeId = "CUST1002";
    }
}
