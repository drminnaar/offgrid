using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Application.Commands.ChangeCustomerStatus;

public sealed record ChangeCustomerStatusCommand : CommandBase
{
    [Required(ErrorMessage = "Status is required")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CustomerStatus Status { get; set; }

    public void Deconstruct(out CustomerStatus status)
    {
        status = Status;
    }
}
