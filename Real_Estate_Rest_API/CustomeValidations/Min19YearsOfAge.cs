using Real_Estate_Rest_API.Data.Entities.Customer_Related;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Real_Estate_Rest_API.CustomeValidations
{
    public class Min19YearsOfAge : ValidationAttribute
    {
        private readonly DateTime CURRENT_DATE = DateTime.Today;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var age = 0;
                var customerDetails = validationContext.ObjectInstance as Customer;
                age = CURRENT_DATE.Year - customerDetails.dateOfBirth.Year;

                return (age >= 19)
                ? ValidationResult.Success :
                new ValidationResult("Age must be 19 years or older for authorization");
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

    }
}