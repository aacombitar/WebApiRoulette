using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiRoulette.Helpers
{
    public class ValidateColorBets : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value== null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            if (value.ToString().ToLower() == "negro" || value.ToString().ToLower() == "rojo") 
            {
              return ValidationResult.Success;
            }
            return new ValidationResult("El color debe ser negro o rojo");

        }
    }
}
