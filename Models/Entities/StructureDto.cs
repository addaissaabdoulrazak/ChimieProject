using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ChimieProject.Models.Entities
{
    public class StructureDto
    {
        public string Acronyme { get; set; }



        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]

        public string Email { get; set; }

        public string Etablissement { get; set; }

        [Key]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Type Structure")]
        public string Type { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Responsable")]
        public string Responsable { get; set; }

        [Display(Name = "Telephone")]
        [Required]
        public string Tel { get; set; }




        public StructureDto() { }

   
    }
}
