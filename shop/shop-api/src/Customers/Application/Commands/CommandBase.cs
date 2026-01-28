using System.ComponentModel.DataAnnotations;

namespace Offgrid.Customers.Application.Commands;

public abstract record CommandBase
{
    protected CommandBase()
    {
    }

    public virtual bool TryValidate(out IReadOnlyDictionary<string, List<string>> errors)
    {
        var validationContext = new ValidationContext(this);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(
            this,
            validationContext,
            validationResults,
            validateAllProperties: true);

        if (isValid)
        {
            errors = new Dictionary<string, List<string>>();
            return true;
        }

        static string ToCamelCase(string input) => (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            ? input
            : char.ToLowerInvariant(input[0]) + input.Substring(1);

        errors = validationResults
            .GroupBy(v => v.MemberNames.FirstOrDefault() ?? "")
            .ToDictionary(
                g => ToCamelCase(g.Key),
                g => g.Select(v => v.ErrorMessage ?? "Invalid value").ToList()
            )
            .AsReadOnly();

        return false;
    }
}
