using Offgrid.Framework.System;

namespace Offgrid.Framework.Exceptions;

[Serializable]
public sealed class ValidationException : Exception
{
    public IReadOnlyDictionary<string, List<string>> Errors { get; }

    public ValidationException(string message) : base(message)
    {
        Errors = new Dictionary<string, List<string>>().AsReadOnly();
    }

    public ValidationException(string message, params ValidationError[] errors) : base(message)
    {
        Errors = errors.ToDictionary(e => e.Property, e => e.Messages).AsReadOnly();
    }

    public ValidationException(string message, IReadOnlyDictionary<string, List<string>> errors) : base(message)
    {
        Errors = errors;
    }

    public ValidationException(string message, params KeyValuePair<string, List<string>>[] errors) : base(message)
    {
        Errors = errors.ToDictionary(e => e.Key, e => e.Value).AsReadOnly();
    }

    public sealed record ValidationError
    {
        public string Property { get; }
        public List<string> Messages { get; }

        public ValidationError(string property, params string[] messages) : this(property, messages.ToList())
        {
        }

        public ValidationError(string property, List<string> messages)
        {
            Property = property.ToCamelCase();
            Messages = messages;
        }
    }
}
