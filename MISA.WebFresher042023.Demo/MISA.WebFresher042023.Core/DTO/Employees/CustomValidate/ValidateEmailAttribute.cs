using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Employees.CustomValidate
{
    public class ValidateEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && value.ToString()?.Trim() != string.Empty)
            {
                var regexAttribute = new RegularExpressionAttribute($"{Resources.ResourceVN.Regex_Email}");
                if (!regexAttribute.IsValid(value))
                {
                    return new ValidationResult(Resources.ResourceVN.Validate_Email_ErrorFormat);
                }
            }
            return ValidationResult.Success;
        }
    }
}
