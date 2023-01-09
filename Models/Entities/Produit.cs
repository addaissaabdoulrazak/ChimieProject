using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace ChimieProject.Models.Entities
{
    public class Produit
    {

        [Required]
        [Display(Name = "CAS")]
        public string CAS { get; set; }
        [Required]
        [Display(Name = "Etat Physique")]
        public string EtatPhysique { get; set; }
        [Required]
        [Display(Name = "Formule")]
        public string Formule { get; set; }

        [Key]
        public long Id { get; set; }
        [Required]
        public string Nom { get; set; }

        public Produit() { }

        public Produit(DataRow dataRow)
        {
            CAS = Convert.ToString(dataRow["CAS"]);
            EtatPhysique = Convert.ToString(dataRow["EtatPhysique"]);
            Formule = Convert.ToString(dataRow["Formule"]);
            Id = Convert.ToInt64(dataRow["Id"]);
            Nom = Convert.ToString(dataRow["Nom"]);
        }
    }
}
