using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Offgrid.Framework.System.ComponentModel.DataAnnotations;

namespace Offgrid.Portal.Customers.Application.Commands.SuspendCustomer;

public sealed record SuspendCustomerCommand : ValidatableBase
{
    [Required(ErrorMessage = "Suspension reason is required")]
    [StringLength(200, MinimumLength = 1)]
    public string Reason { get; set; } = string.Empty;

    [JsonIgnore]
    [BindNever]
    public string SuspendedBy { get; set; } = string.Empty;

    public void Deconstruct(out string reason, out string suspendedBy)
    {
        suspendedBy = SuspendedBy;
        reason = Reason;
    }

    public override bool TryValidate(out IReadOnlyDictionary<string, List<string>> errors)
    {
        var isValid = base.TryValidate(out errors);

        var additionalErrors = new Dictionary<string, List<string>>();

        if (string.IsNullOrWhiteSpace(SuspendedBy))
        {
            additionalErrors["suspendedBy"] = ["The username of the person suspending the customer is required."];
            isValid = false;
        }

        if (additionalErrors.Count > 0)
        {
            errors = errors.Concat(additionalErrors).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        return isValid;
    }
}
