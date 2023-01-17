using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Xml.Linq;

namespace ChimieProject.Models.Entities
{
    public class EchangeLot
    {
        [Required]
        public string Concentration { get; set; }


        [Required(ErrorMessage = "The Peremption Date  is required")]
        [Display(Name = "Date Peremption")]
        public DateTime? DatePeremption { get; set; }


        [Display(Name = "Date Publication")]
        [Required(ErrorMessage = "The Publication Date  is required")]
        public DateTime DatePublication { get; set; }


        [Key]
        public long Id { get; set; }


        [ForeignKey("IdLabo")]
        public long IdLabo { get; set; }


        [ForeignKey("IdProduit")]
        public long IdProduit { get; set; }



        [Required]
        public string Type { get; set; }



        [Required]
        public string Purete { get; set; }


        [Required]
        public Single? Quantite { get; set; }


        [Required]
        public string UniteQuantite { get; set; }


        public EchangeLot() { }

        public EchangeLot(DataRow dataRow)
        {
            Concentration = (dataRow["Concentration"] == System.DBNull.Value) ? "" : Convert.ToString(dataRow["Concentration"]);
            DatePeremption = (dataRow["DatePeremption"] == System.DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(dataRow["DatePeremption"]);
            DatePublication = Convert.ToDateTime(dataRow["DatePublication"]);
            Id = Convert.ToInt64(dataRow["Id"]);
            IdLabo = Convert.ToInt64(dataRow["IdLabo"]);
            IdProduit = Convert.ToInt64(dataRow["IdProduit"]);
            Type = (dataRow["Type"] == System.DBNull.Value) ? "" : Convert.ToString(dataRow["Type"]);
            Purete = (dataRow["Purete"] == System.DBNull.Value) ? "" : Convert.ToString(dataRow["Purete"]);
            Quantite = (dataRow["Quantite"] == System.DBNull.Value) ? (Single?)null : Convert.ToSingle(dataRow["Quantite"]);
            UniteQuantite = (dataRow["UniteQuantite"] == System.DBNull.Value) ? "" : Convert.ToString(dataRow["UniteQuantite"]);
        }
    }
}
