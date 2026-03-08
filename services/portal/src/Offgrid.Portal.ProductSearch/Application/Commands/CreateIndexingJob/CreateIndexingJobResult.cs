namespace Offgrid.Portal.ProductSearch.Application.Commands.CreateIndexingJob;

public sealed record CreateIndexingJobResult
{
    public CreateIndexingJobResult(Guid jobId)
    {
        JobId = jobId;
    }

    public Guid JobId { get; private set; }
}
