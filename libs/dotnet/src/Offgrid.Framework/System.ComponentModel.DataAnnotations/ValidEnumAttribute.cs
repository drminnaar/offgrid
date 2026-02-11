using System.ComponentModel.DataAnnotations;

namespace Offgrid.Framework.System.ComponentModel.DataAnnotations;

public sealed class ValidEnumAttribute : ValidationAttribute
{
    private readonly Type _enumType;

    public ValidEnumAttribute(Type enumType)
    {
        _enumType = enumType;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        var stringValue = value.ToString();

        if (string.IsNullOrWhiteSpace(stringValue))
            return new ValidationResult($"The {validationContext.DisplayName} field is required.");

        var enumValues = Enum.GetNames(_enumType);

        if (Enum.TryParse(_enumType, stringValue, ignoreCase: true, out var result) &&
            Enum.IsDefined(_enumType, result))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(
            $"Invalid value for {validationContext.DisplayName}. Valid values are: {string.Join(", ", enumValues)}");
    }
}
