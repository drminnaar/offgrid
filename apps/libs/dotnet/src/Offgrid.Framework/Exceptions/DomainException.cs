namespace Offgrid.Framework.Exceptions;

[Serializable]
public sealed class DomainException : Exception
{
    public IReadOnlyDictionary<string, List<string>> Errors { get; }

    public DomainException(string message) : base(message)
    {
        Errors = new Dictionary<string, List<string>>().AsReadOnly();
    }

    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
        Errors = new Dictionary<string, List<string>>().AsReadOnly();
    }

    public DomainException(string message, params KeyValuePair<string, List<string>>[] errors) : base(message)
    {
        Errors = errors.ToDictionary(e => e.Key, e => e.Value).AsReadOnly();
    }
}
