using AuthenticationProject.Models.BLL;
using ChimieProject.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChimieProject.Models
{
    public class EmailUserUniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IEnumerable<User> objectUserlist = BLL_User.GetAll();

            foreach(var obj in objectUserlist)
            {
                if (obj.Email.Equals(value))
                {
                    
                    return new ValidationResult(GetErrorMessage((string)value));

                }
            }


            return ValidationResult.Success;
        }

        public string GetErrorMessage(string email)
        {
            return $"Email {email} is already in use.";
        }
    }
}
