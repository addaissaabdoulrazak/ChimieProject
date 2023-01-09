using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ChimieProject.Models.Entities
{
    public class Laboratoire
    {
        public string Acronyme { get; set; }

        public string Email { get; set; }

        public string Etablissement { get; set; }
        public long Id { get; set; }

        public string Nom { get; set; }
        public string Password { get; set; }

        public string Responsable { get; set; }

        public string Tel { get; set; }

        public int Statut { get; set; }

        public string Role { get; set; }

        public Laboratoire() { }

        public Laboratoire(DataRow dataRow)
        {
            Acronyme = Convert.ToString(dataRow["Acronyme"]);
            Email = Convert.ToString(dataRow["Email"]);
            Etablissement = Convert.ToString(dataRow["Etablissement"]);
            Id = Convert.ToInt64(dataRow["Id"]);
            Statut = Convert.ToInt32(dataRow["Statut"]);
            Role = Convert.ToString(dataRow["Role"]);
            Nom = Convert.ToString(dataRow["Nom "]);
            Password = Convert.ToString(dataRow["Password "]);
            Responsable = Convert.ToString(dataRow["Responsable "]);
            Tel = Convert.ToString(dataRow["Tel "]);
        }
    }
}
