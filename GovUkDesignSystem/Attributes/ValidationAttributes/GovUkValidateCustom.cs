using System;
using System.ComponentModel.DataAnnotations;

namespace GovUkDesignSystem.Attributes.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class GovUkValidateCustom: ValidationAttribute
{
    public string CustomValidationPropertyName;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var propertyInfo = validationContext.ObjectInstance.GetType().GetProperty(CustomValidationPropertyName);
            
        if (propertyInfo is null)
        {
            throw new ArgumentException(
                $"'{CustomValidationPropertyName}' must be a boolean property in the model the attribute is included in");
        }
            
        var isValid = (bool)propertyInfo.GetValue(validationContext.ObjectInstance, null)!;
            
        return isValid ? ValidationResult.Success : new ValidationResult(ErrorMessage) ;
    }
}
