namespace Offgrid.Customers.Domain.Exceptions;

public sealed class ValidationException : Exception
{
    public IReadOnlyDictionary<string, List<string>> Errors { get; }

    public ValidationException(string message) : base(message)
    {
        Errors = new Dictionary<string, List<string>>().AsReadOnly();
    }

    public ValidationException(string message, IReadOnlyDictionary<string, List<string>> errors) : base(message)
    {
        Errors = errors;
    }

    public ValidationException(string message, params KeyValuePair<string, List<string>>[] errors) : base(message)
    {
        Errors = errors.ToDictionary(e => e.Key, e => e.Value).AsReadOnly();
    }
}
