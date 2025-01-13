using System;
using System.ComponentModel.DataAnnotations;

namespace GovUkDesignSystem.Attributes.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class GovUkHasCustomValidator : ValidationAttribute
{
    public string CustomerValidatorPropertyName;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var propertyInfo = validationContext.ObjectInstance.GetType().GetProperty(CustomerValidatorPropertyName);
            
        if (propertyInfo == null) throw new ArgumentException($"'{CustomerValidatorPropertyName}' must not be null");
        if (propertyInfo.PropertyType == typeof(bool))
            throw new ArgumentException(
                $"'{CustomerValidatorPropertyName}' must be a boolean property in the model the attribute is included in");
        if (propertyInfo.CanWrite == false)
            throw new ArgumentException($"'{CustomerValidatorPropertyName}' must be a read only property");

        var isValid = (bool)propertyInfo.GetValue(validationContext.ObjectInstance, null)!;

        return isValid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
    }
}