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

        [Required]
        public string Email { get; set; }

        [Required]
        public string Etablissement { get; set; }

        [Key]
        public long Id { get; set; }

        public string Nom { get; set; }

        [Required]
        [Display(Name = "Type Structure")]

        public string Type { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Responsable { get; set; }

        [Required]
        public string Tel { get; set; }




        public StructureDto() { }

   
    }
}
