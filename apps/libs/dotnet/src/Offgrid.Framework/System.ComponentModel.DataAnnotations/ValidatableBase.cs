using System.ComponentModel.DataAnnotations;

namespace Offgrid.Framework.System.ComponentModel.DataAnnotations;

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

        errors = validationResults
            .GroupBy(v => v.MemberNames.FirstOrDefault() ?? "")
            .ToDictionary(
                g => g.Key.ToCamelCase(),
                g => g.Select(v => v.ErrorMessage ?? "Invalid value").ToList()
            )
            .AsReadOnly();

        return false;
    }
}