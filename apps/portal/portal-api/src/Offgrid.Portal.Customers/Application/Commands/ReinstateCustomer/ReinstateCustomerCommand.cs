using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Offgrid.Framework.System.ComponentModel.DataAnnotations;

namespace Offgrid.Portal.Customers.Application.Commands.ReinstateCustomer;

public sealed record ReinstateCustomerCommand : ValidatableBase
{
    [Required(ErrorMessage = "Reinstatement reason is required")]
    [StringLength(200, MinimumLength = 1)]
    public string Reason { get; set; } = string.Empty;

    [JsonIgnore]
    [BindNever]
    public string ReinstatedBy { get; set; } = string.Empty;

    public void Deconstruct(out string reason, out string reinstatedBy)
    {
        reinstatedBy = ReinstatedBy;
        reason = Reason;
    }

    public override bool TryValidate(out IReadOnlyDictionary<string, List<string>> errors)
    {
        var isValid = base.TryValidate(out errors);

        var additionalErrors = new Dictionary<string, List<string>>();

        if (string.IsNullOrWhiteSpace(ReinstatedBy))
        {
            additionalErrors["reinstatedBy"] = ["The username of the person reinstating the customer is required."];
            isValid = false;
        }

        if (additionalErrors.Count > 0)
        {
            errors = errors.Concat(additionalErrors).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        return isValid;
    }
}
