﻿using System.ComponentModel.DataAnnotations;

namespace offers.itacademy.ge.API.Attributes
{
    public class ValidEndDateAttribute : ValidationAttribute
    {
        public ValidEndDateAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.UtcNow;
            }
            return false;
        }
    }
}
