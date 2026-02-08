namespace Offgrid.Framework.Domain;

public record ValueChanges(
    string ChangedBy,
    IReadOnlyCollection<Change> Changes
) : IHasValueChanges;
