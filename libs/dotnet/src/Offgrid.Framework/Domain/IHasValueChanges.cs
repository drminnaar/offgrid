namespace Offgrid.Framework.Domain;

public interface IHasValueChanges
{
    public string ChangedBy { get; }
    public IReadOnlyCollection<Change> Changes { get; }
}
