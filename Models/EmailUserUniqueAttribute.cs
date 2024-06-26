﻿
using ChimieProject.Models.BLL;
using ChimieProject.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChimieProject.Models
{
    public class EmailUserUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var owner = validationContext.ObjectInstance as StructureDto;
            if (owner == null) return new ValidationResult("Model is empty");


            var user = BLL_Structure.GetElementByEmail(owner.Email);

            if (user == null)
                return ValidationResult.Success;
            else
                return new ValidationResult("Cette addrese Email existe déjà");
        }

      
    }
}
