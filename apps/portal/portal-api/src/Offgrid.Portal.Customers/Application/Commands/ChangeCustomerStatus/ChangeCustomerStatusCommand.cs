using System.ComponentModel.DataAnnotations;
using Offgrid.Framework.System.ComponentModel.DataAnnotations;

namespace Offgrid.Portal.Customers.Application.Commands.ChangeCustomerStatus;

public sealed record ChangeCustomerStatusCommand : ValidatableBase
{
    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; } = string.Empty;

    public void Deconstruct(out string status)
    {
        status = Status;
    }
}
