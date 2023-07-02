using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Employees.CustomValidate
{
    public class ValidateDatetimeAttribute : ValidationAttribute
    {
        private readonly string _errorMessageResourceKey;

        public ValidateDatetimeAttribute(string errorMessageResourceKey)
        {
            _errorMessageResourceKey = errorMessageResourceKey;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && value is DateTime dateValue)
            {
                if (dateValue > DateTime.Now)
                {
                    string? errorMessage = Resources.ResourceVN.ResourceManager.GetString(_errorMessageResourceKey);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
