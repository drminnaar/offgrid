using System.ComponentModel.DataAnnotations;
using Offgrid.Framework.System.ComponentModel.DataAnnotations;

namespace Offgrid.Shop.Customers.Application.Commands.UpsertCustomer;

public sealed record UpsertCustomerCommand : ValidatableBase
{
    private string _email = string.Empty;
    private string _fullName = string.Empty;
    private string _keycloakUserId = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(200, MinimumLength = 5)]
    public string Email
    {
        get => _email;
        set => _email = value?.Trim().ToLower() ?? string.Empty;
    }

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(200, MinimumLength = 5)]
    public string FullName
    {
        get => _fullName;
        init => _fullName = value?.Trim() ?? string.Empty;
    }

    [Required(ErrorMessage = "Keycloak User ID is required")]
    [StringLength(200, MinimumLength = 5)]
    public string KeycloakUserId
    {
        get => _keycloakUserId;
        init => _keycloakUserId = value?.Trim() ?? string.Empty;
    }

    public void Deconstruct(out string email, out string fullName, out string keycloakUserId)
    {
        email = Email;
        fullName = FullName;
        keycloakUserId = KeycloakUserId;
    }
}
