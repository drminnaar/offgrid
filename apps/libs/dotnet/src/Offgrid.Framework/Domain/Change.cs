namespace Offgrid.Framework.Domain;

public sealed record Change
{
    public string PropertyName { get; }
    public object? OldValue { get; }
    public object? NewValue { get; }
    public IReadOnlyCollection<string> Reasons { get; }

    public Change(string propertyName, object? oldValue, object? newValue, params string[] reasons)
    {
        if (string.IsNullOrWhiteSpace(propertyName))
        {
            throw new ArgumentException("Property name is required.", nameof(propertyName));
        }

        PropertyName = propertyName;
        OldValue = oldValue;
        NewValue = newValue;
        Reasons = reasons?.ToArray() ?? [];
    }
}
