using Offgrid.Framework.Exceptions;
using Offgrid.Framework.System;
using Offgrid.Shop.Customers.Domain.Services;

namespace Offgrid.Shop.Customers.Domain.Entities;

public sealed class Customer
{
    public Guid CustomerId { get; private set; }
    public string CustomerNumber { get; private set; } = string.Empty;
    public string KeycloakUserId { get; private set; } = string.Empty;
    public CustomerStatus Status { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateTimeOffset CreatedDate { get; private set; }
    public DateTimeOffset? UpdatedDate { get; private set; }
    public DateTimeOffset? DeletedDate { get; private set; }

    private Customer()
    {
    }

    public static Customer Create(
        string keycloakUserId,
        string email,
        string fullName,
        TimeProvider timeProvider,
        ICustomerIdGenerator customerIdGenerator,
        ICustomerNumberGenerator customerNumberGenerator)
    {
        var (firstName, lastName) = NormalizeFullName(fullName);

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            throw new DomainException("Full name must include both first and last names",
            [
                new("FullName", ["Full name must include both first and last names"])
            ]);
        }

        return Create(
            keycloakUserId,
            email,
            firstName,
            lastName,
            timeProvider,
            customerIdGenerator,
            customerNumberGenerator);
    }

    public void Activate(TimeProvider timeProvider)
    {
        Status = CustomerStatus.Active;
        UpdatedDate = timeProvider.GetUtcNow();
    }

    public void Suspend(TimeProvider timeProvider)
    {
        Status = CustomerStatus.Suspended;
        UpdatedDate = timeProvider.GetUtcNow();
    }

    public void Update(string fullName, string email, TimeProvider timeProvider)
    {
        var (firstName, lastName) = NormalizeFullName(fullName);

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            throw new DomainException("Full name must include both first and last names",
            [
                new("FullName", ["Full name must include both first and last names"])
            ]);
        }

        Email = email.Trim().ToLower();
        FirstName = firstName;
        LastName = lastName;
        UpdatedDate = timeProvider.GetUtcNow();

        Validate();
    }

    private void Validate()
    {
        var errors = new Dictionary<string, List<string>>();

        if (string.IsNullOrWhiteSpace(FirstName))
            AddError(errors, nameof(FirstName), "First name is required");

        if (FirstName.Length > 50)
            AddError(errors, nameof(FirstName), "First name may not exceed 50 characters");

        if (FirstName.Length < 2)
            AddError(errors, nameof(FirstName), "First name must be at least 2 characters");

        if (FirstName.Any(char.IsDigit))
            AddError(errors, nameof(FirstName), "First name may not contain numbers");

        if (string.IsNullOrWhiteSpace(LastName))
            AddError(errors, nameof(LastName), "Last name is required");

        if (LastName.Length < 2)
            AddError(errors, nameof(LastName), "Last name must be at least 2 characters");

        if (LastName.Length > 50)
            AddError(errors, nameof(LastName), "Last name may not exceed 50 characters");

        if (LastName.Any(char.IsDigit))
            AddError(errors, nameof(LastName), "Last name may not contain numbers");

        if (string.IsNullOrWhiteSpace(Email))
            AddError(errors, nameof(Email), "Email is required");

        if (!IsValidEmail(Email))
            AddError(errors, nameof(Email), "Invalid email format");

        if (errors.Count != 0)
        {
            throw new DomainException("Customer validation failed", [.. errors]);
        }
    }


    private static void AddError(Dictionary<string, List<string>> errors, string key, string message)
    {
        if (errors.TryGetValue(key, out var value))
        {
            value.Add(message);
        }
        else
        {
            errors[key] = [message];
        }
    }

    private static Customer Create(
        string keycloakUserId,
        string email,
        string firstName,
        string lastName,
        TimeProvider timeProvider,
        ICustomerIdGenerator customerIdGenerator,
        ICustomerNumberGenerator customerNumberGenerator)
    {
        var customer = new Customer
        {
            CustomerId = customerIdGenerator.GenerateCustomerId(),
            CustomerNumber = customerNumberGenerator.GenerateCustomerNumber(),
            KeycloakUserId = keycloakUserId.Trim(),
            Email = email.Trim().ToLower(),
            FirstName = firstName.ToTitleCase(),
            LastName = lastName.ToTitleCase(),
            Status = CustomerStatus.Active,
            CreatedDate = timeProvider.GetUtcNow()
        };

        customer.Validate();

        return customer;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static (string FirstName, string LastName) NormalizeFullName(string fullName)
    {
        var value = fullName.Trim();
        var (firstName, lastName) = SplitFullName(value);
        return (firstName.ToTitleCase(), lastName.ToTitleCase());
    }

    private static (string FirstName, string LastName) SplitFullName(string fullName)
    {
        var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length switch
        {
            0 => (string.Empty, string.Empty),
            1 => (parts[0].Trim(), string.Empty),
            _ => (parts[0].Trim(), string.Join(" ", parts.Skip(1)).Trim())
        };
    }
}
