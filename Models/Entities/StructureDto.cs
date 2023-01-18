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

        public string Email { get; set; }

        public string Etablissement { get; set; }

        [Key]
        public long Id { get; set; }

        public string Nom { get; set; }

        public string Type { get; set; }

        public string Password { get; set; }

        public string Responsable { get; set; }

        public string Tel { get; set; }




        public StructureDto() { }

   
    }
}
