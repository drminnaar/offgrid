using Offgrid.Framework.System;
using Offgrid.Framework.System.ComponentModel.DataAnnotations;
using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Application.Queries.GetAllCustomers;

public sealed record GetAllCustomersQuery : ValidatableBase
{
    public const int DefaultPageSize = 10;
    public const int DefaultPageNumber = 1;

    public int PageSize { get; init; } = DefaultPageSize;

    public int PageNumber { get; init; } = DefaultPageNumber;

    public string Status { get; init; } = string.Empty;

    public CustomerStatus? ParsedStatus => Enum.TryParse<CustomerStatus>(Status, true, out var parsed) ? parsed : null;

    public override bool TryValidate(out IReadOnlyDictionary<string, List<string>> errors)
    {
        var isValid = base.TryValidate(out errors);

        var additionalErrors = new Dictionary<string, List<string>>();

        if (PageSize <= 0)
        {
            additionalErrors[nameof(PageSize)] = ["Page size must be greater than zero."];
            isValid = false;
        }

        if (PageNumber <= 0)
        {
            additionalErrors[nameof(PageNumber)] = ["Page number must be greater than zero."];
            isValid = false;
        }

        if (!string.IsNullOrWhiteSpace(Status) && !Enum.TryParse<CustomerStatus>(Status, true, out _))
        {
            additionalErrors[nameof(Status)] = [$"Status must be one of {EnumExtensions.ToCommaSeparatedList<CustomerStatus>()}."];
            isValid = false;
        }

        if (additionalErrors.Count > 0)
        {
            errors = errors.Concat(additionalErrors).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        return isValid;
    }
}
